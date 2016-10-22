using System;
using System.Collections.Generic;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.DAL.Concrete.Repository;
using EnglishGrammar.Entities;
using System.Windows.Input;
using System.Linq;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfAppEnglishGrammar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private readonly ITestRepository _testRepository;
        private readonly IMarkRepository _markRepository;
        // Review - Oleg Shandra: Write private modifier to each private field
        TestStardedPage testPage;
        WindowGenerator generator;
        Random rand ;

        #endregion

        #region Constructor  
             
        public MainWindow()
        {
            InitializeComponent();
            generator = new WindowGenerator(this);
            string connectionString = ConfigurationManager.ConnectionStrings["EnglishJediConnection"].ConnectionString;
            try
            {                
                _testRepository = new TestRepository(connectionString);
                if (_testRepository == null)
                    throw new NoConnectionToDBException();
                    _markRepository = new MarkRepository(connectionString);
                if (_markRepository == null)
                    throw new NoConnectionToDBException();
            }
            catch(NoConnectionToDBException e)
            {
                MessageBox.Show("Lost connection to DB! "+e.Message);
            }
          
        }

        #endregion

        #region Window

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tabItem = mainTabControl.SelectedItem as TabItem;
            
            switch (tabItem.Name)
            {
                case "tcShowAllTests":
                    generator.FillTest(_testRepository);
                    break;

                case "tcPersonName":
                    generator.FillPersonalInfo(_markRepository);
                    break;

                case "tcShowRating":
                    generator.FillRating(_markRepository);
                    break;
                case "tcShowThemeRating":
                    generator.FillThemeRating(_markRepository);
                    break;
                default:
                    return;
            }
        }

        #region EventMethod
        public void TestCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Canvas).Background = new SolidColorBrush(Color.FromRgb(0x09, 0xA9, 0xE5));
        }
        public void TestCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Canvas).Background = new SolidColorBrush(Color.FromRgb(0xE0, 0xE9, 0xF5));
        }
        public void TestCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TestCanvas clickedCanvas = sender as TestCanvas;
            AddNewTabItem(clickedCanvas.test);
        }
        public void TestStartButton_Click(object sender, RoutedEventArgs e)
        {
            testPage = new TestStardedPage((sender as Button).Parent as Grid, (((sender as Button).Parent as Grid).Parent as TestTabItem).test, this, SaveMark);
            (((sender as Button).Parent as Grid).Parent as TestTabItem).isTestStarted = true;
            testPage.Create();
            testPage.Start();
        }
        public void TestCloseButton_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedItem = tcShowAllTests;
            mainTabControl.Items.Remove(((sender as Button).Parent as Grid).Parent as TestTabItem);
        }
        #endregion
        
        // Review - Oleg Shandra: Input parameters without underscore
        private Mark SaveMark(Mark _mark)
        {
            _markRepository.InsertMark(_mark);
            return null;
        }
       
        public void AddNewTabItem(Test test)
        {
            foreach (TabItem x in mainTabControl.Items) //check if tab for thet test alredy exist
            {
                if (x is TestTabItem && (x as TestTabItem).test.Id == test.Id)
                {
                    generator.StartGenerateTest(x as TestTabItem);
                    return;
                }
            }
            TestTabItem newTabItem = new TestTabItem
            {
                test = test,
                Header = test.Name,
                Name = "Test" + test.Id
            };
            mainTabControl.Items.Add(newTabItem);
            generator.StartGenerateTest(newTabItem);
        }

        private Brush RandomColor()
        {
            return new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));
        }

        #endregion

    }
    
    // Review - Oleg Shandra: Why are these classes here? Why are they not in single files?
    // Review - Oleg Shandra: Write all public fields with a capital letter.
    // Review - Oleg Shandra: Class (in your case controllers) can contain only those fields that describe it, and not something else
    #region AdditionalClasses
    internal class TestCanvas : Canvas
    {
        public Test test { get; set; }
    }
  
    public class TestTabItem : TabItem
    {
        public Test test { get; set; }
        public bool isTestStarted = false;
    }
    public class NextButton : Button 
    {
        public Queue<Question> questionQueue = new Queue<Question>();
        public Question question = new Question() { };
    }
   
    public class AnswerRadioButton : RadioButton 
    {
        public Answer answer;
    }
    #endregion
}
