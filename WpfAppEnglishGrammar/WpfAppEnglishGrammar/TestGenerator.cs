using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.Entities;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfAppEnglishGrammar
{
    public class TestStardedPage 
    {
        /// <summary>
        /// Interaction logic for and test-passing process  generation
        /// </summary>
        #region Fields
        // Review TK: It isn't a good practice to have a public fields within class.
        // It is a violation of sOlid principles.
        public MainWindow mainform;
        public Grid answerGrid;

        public Test test;
        public List<Answer> answers = new List<Answer>();
        public Mark testMark;

        public DispatcherTimer testTimer;
        public DateTime endTime;

        public TextBlock textBlockDescription, textQwestion, timerDisplayTextBlock;
        public NextButton testNextButton;

        public List<AnswerRadioButton> answersRadioButtuns = new List<AnswerRadioButton>() { };
        public List<TextBlock> answersTextBlocks = new List<TextBlock>() { };
        public Queue<Question> _questionQueue;
        // Review TK: Please don't forget about naming for fields.
        private Func<Mark, Mark> Callback;

        #endregion

        // Review TK: Naming for parameters.
        #region Constructor
        public TestStardedPage(Grid _grid, Test _test, MainWindow _mainform, Func<Mark, Mark> _Callback)
        {
            answerGrid = _grid;
            test = _test;
            mainform = _mainform;
            Callback = _Callback;
            // Review TK: It seems that this field isn't used.
            Random rand = new Random();
        }
        #endregion

        public void CleareGrid()
        {
            answerGrid.Children.Clear();
            answerGrid.RowDefinitions.Clear();
            answerGrid.ColumnDefinitions.Clear();

        }

        public void Create()
        {
            CleareGrid();
            answerGrid.Margin = new Thickness(20, 0, 15, 60);

            endTime = DateTime.Now + test.Duration;
            // createControls
            #region Сontrols
            // Review TK: It would be great to have this logic within View. I mean *.xaml.
            // For the future, please use string.Format or string interpolation.
            ColumnDefinitionCollection cDefs = answerGrid.ColumnDefinitions;
            cDefs.Add(new ColumnDefinition() { Name = "tbTest" + test.Id + "tbTestTextRadioButtonsColumn" });
            cDefs.Add(new ColumnDefinition() { Name = "tbTest" + test.Id + "tbTestTextAnswersColumn" });
            cDefs.Add(new ColumnDefinition() { Name = "tbTest" + test.Id + "tbTestTextNextButtonColumn" });

            RowDefinitionCollection rDefs = answerGrid.RowDefinitions;
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextDesriptionRow", Height = new GridLength(0.7, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextQuestionRow", Height = new GridLength(0.6, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextAnswer1Row", Height = new GridLength(0.5, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextAnswer2Row", Height = new GridLength(0.5, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextAnswer3Row", Height = new GridLength(0.5, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextAnswer4Row", Height = new GridLength(0.5, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "tbTestTextNextButtonRow" });

            //Description TextBlock
            textBlockDescription = new TextBlock();
            textBlockDescription.Name = "textBlockTest" + test.Id + "Description";
            textBlockDescription.Text = test.Description;
            textBlockDescription.FontSize = 18;
            textBlockDescription.Width = answerGrid.Width - 10;
            textBlockDescription.TextWrapping = TextWrapping.Wrap;
            textBlockDescription.Foreground = new SolidColorBrush(Color.FromArgb(255, 184, 215, 232));//dark gray
            textBlockDescription.VerticalAlignment = VerticalAlignment.Center;
            textBlockDescription.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumnSpan(textBlockDescription, 3);
            Grid.SetColumn(textBlockDescription, 0);
            Grid.SetRow(textBlockDescription, 0);
            answerGrid.Children.Add(textBlockDescription);

            //timer 
            timerDisplayTextBlock = new TextBlock()
            {
                Name = "textBlockTest" + test.Id + "Timer",
                Foreground = new SolidColorBrush(Colors.Orange),
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new FontFamily("Franklin Gothic Medium"),

                HorizontalAlignment = HorizontalAlignment.Left,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };
            Grid.SetColumn(timerDisplayTextBlock, 0);
            Grid.SetRow(timerDisplayTextBlock, 6);
            answerGrid.Children.Add(timerDisplayTextBlock);

            _questionQueue = new Queue<Question>();
            foreach (Question qwestion in test.Questions)
            {
                _questionQueue.Enqueue(qwestion);
            }

            testNextButton = new NextButton()
            {
                Style = mainform.FindResource("ButtonStyle") as Style,
                Content = "Next",
                question = _questionQueue.Dequeue(),
                questionQueue = _questionQueue,
            };
            testNextButton.Click += TestNextButton_Click;
            Grid.SetColumn(testNextButton, 3);
            Grid.SetRow(testNextButton, 6);
            answerGrid.Children.Add(testNextButton);
            //Qwestion text

            textQwestion = new TextBlock()
            {
                Name = "textBlockTest" + test.Id + "Question",
                Text = testNextButton.question.QuestionText,
                FontFamily = new FontFamily("Franklin Gothic Medium"),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 94, 219, 239)),//black
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextAlignment = TextAlignment.Left,
                TextWrapping = TextWrapping.Wrap
            };
            Grid.SetColumnSpan(textQwestion, 3);
            Grid.SetColumn(textQwestion, 0);
            Grid.SetRow(textQwestion, 1);
            answerGrid.Children.Add(textQwestion);


            int answerRow = 2;
            foreach (Answer qustionAnswer in testNextButton.question.Answers)
            {
                AnswerRadioButton tempAnswerRadioButton = new AnswerRadioButton()
                {
                    Name = "answer" + qustionAnswer.Id,
                    answer = qustionAnswer,
                    GroupName = "answers"
                };
                Viewbox answerVb = new Viewbox()
                {
                    Name = "answerViewbox" + qustionAnswer.Id,
                    Height = 40
                };
                answerVb.Child = tempAnswerRadioButton;
                Grid.SetColumn(answerVb, 0);
                Grid.SetRow(answerVb, answerRow);
                answerGrid.Children.Add(answerVb);

                answersRadioButtuns.Add(tempAnswerRadioButton);
                TextBlock tempAnswerTextBlock = new TextBlock()
                {
                    Name = "answerTest" + test.Id,
                    Text = qustionAnswer.QuestionAnswer,
                    FontFamily = new FontFamily("Franklin Gothic Medium"),
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 94, 219, 239)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.Wrap
                };
                Grid.SetColumnSpan(tempAnswerTextBlock, 2);
                Grid.SetColumn(tempAnswerTextBlock, 1);
                Grid.SetRow(tempAnswerTextBlock, answerRow);

                answersTextBlocks.Add(tempAnswerTextBlock);
                answerGrid.Children.Add(tempAnswerTextBlock);

                answerRow++;
            }
            #endregion
        }

        public void ShowMark()
        {
            CleareGrid();
            answerGrid.Margin = new Thickness(50, 0, 50, 145);
            answerGrid.Background = new SolidColorBrush(Color.FromArgb(0x7F, 0x62, 0xD8, 0xD8));

            ColumnDefinitionCollection cDefs = answerGrid.ColumnDefinitions;
            cDefs.Add(new ColumnDefinition());

            RowDefinitionCollection rDefs = answerGrid.RowDefinitions;
            rDefs.Add(new RowDefinition() { Height = new GridLength(0.35, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Height = new GridLength(0.5, GridUnitType.Star) });
            rDefs.Add(new RowDefinition() { Height = new GridLength(0.15, GridUnitType.Star) });

            TextBlock textBlockMessage = new TextBlock();
            textBlockMessage.Name = "textBlockTest" + test.Id + "message";
            // Review TK: I would prefer to use constants or resx.
            textBlockMessage.Text = testMark.PercentValue >= test.PassValue ? "THE FORSE IS WITH YOU" : "DID YOU CHOOSE THE DARK SIDE?";
            textBlockMessage.FontSize = testMark.PercentValue >= test.PassValue ? 34 : 30;
            textBlockMessage.Width = answerGrid.Width - 10;
            textBlockMessage.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            textBlockMessage.VerticalAlignment = VerticalAlignment.Center;
            textBlockMessage.TextWrapping = TextWrapping.Wrap;
            textBlockMessage.TextAlignment = TextAlignment.Center;
            textBlockMessage.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetColumn(textBlockMessage, 0);
            Grid.SetRow(textBlockMessage, 0);
            answerGrid.Children.Add(textBlockMessage);

            TextBlock textBlockPersentValue = new TextBlock();
            textBlockPersentValue.Name = "textBlockTest" + test.Id + "Description";
            textBlockPersentValue.Text = testMark.PercentValue.ToString();
            textBlockPersentValue.FontSize = 150;
            textBlockPersentValue.Foreground = testMark.PercentValue >= test.PassValue ? new SolidColorBrush(Color.FromRgb(0, 0xF0, 0)) : new SolidColorBrush(Color.FromRgb(0xF0, 0, 0));
            textBlockPersentValue.VerticalAlignment = VerticalAlignment.Center;
            textBlockPersentValue.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetColumn(textBlockPersentValue, 0);
            Grid.SetRow(textBlockPersentValue, 1);
            answerGrid.Children.Add(textBlockPersentValue);

            testNextButton = new NextButton()
            {
                Style = mainform.FindResource("ButtonStyle") as Style,
                Content = "Close",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            testNextButton.Click += mainform.TestCloseButton_Click;

            Grid.SetColumn(testNextButton, 0);
            Grid.SetRow(testNextButton, 2);
            answerGrid.Children.Add(testNextButton);
        }

        public void Start()
        {
            testTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1), DispatcherPriority.Background, t_Tick, Dispatcher.CurrentDispatcher);
            testTimer.Start();
        }

        public void Update()
        {
            textQwestion.Text = testNextButton.question.QuestionText;
            for (int i = 0; i < testNextButton.question.Answers.Count; i++)
            {
                answersTextBlocks[i].Text = testNextButton.question.Answers[i].QuestionAnswer;
                answersRadioButtuns[i].answer = testNextButton.question.Answers[i];
                answersRadioButtuns[i].IsChecked = false;
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            TimeSpan timeRemains = (endTime - DateTime.Now);

            if (timeRemains < new TimeSpan(0, 0, 0, 0, 10))
            {
                timerDisplayTextBlock.Text = "";
                (sender as DispatcherTimer).Stop();
                Finish();
            }
            else
            {
                timerDisplayTextBlock.Text = Convert.ToString(timeRemains.Minutes.ToString().PadLeft(2, '0') + ':' + timeRemains.Seconds.ToString().PadLeft(2, '0'));
            }
        }

        private void TestNextButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton nextButton = sender as NextButton;
            // Review TK: Possible null reference.
            // A lot of if statements. Reduce method complexity.
            // You can use this https://sourcemaking.com/refactoring
            if (nextButton.question != null)
            {
                if (answersRadioButtuns.Where(b => b.IsChecked.Value).Count() != 0)
                {
                    answers.Add(answersRadioButtuns.Where(b => b.IsChecked.Value).FirstOrDefault().answer);
                }
                if (nextButton.questionQueue.Count != 0)
                {
                    nextButton.question = nextButton.questionQueue.Dequeue();
                    Update();
                }
                else
                {
                    nextButton.Content = "Finish";
                    nextButton.Click -= TestNextButton_Click;
                    Finish();
                }
            }
        }

        private void Finish()
        {
            testTimer.Stop();

            // Review TK: Use var if type of variable is clear from the context.
            Mark returnMark = new Mark()
            {
                TestId = test.Id,
                Test = test,
                UserId = UserConfig.Id
            };
            returnMark.MarkAnswer = new List<MarkAnswers>() { };
            foreach (Answer a in answers)
            {
                returnMark.MarkAnswer.Add(new MarkAnswers()
                {
                    AnswerId = a.Id,
                    Answer = a
                });
            }
            testMark = returnMark;
            ShowMark();
            Callback(testMark);
        }
    }
    // Review TK: It is a good practice to put classes into separated files.
    public class WindowGenerator 
    {
        /// <summary>
        /// Interaction logic for tab items  generation
        /// </summary>
        public MainWindow mainWindow;
        Random rand = new Random();
        public WindowGenerator(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
        }

        /// <summary>
        ///Tab item PersonalInfo generation
        /// </summary>
        public void FillPersonalInfo(IMarkRepository _markRepository)
        {
            try
            {
                // Review TK: You could use string.Format or string interpolation.
                mainWindow.textBlockName.Text = "First Name:     " + UserConfig.FirstName;
                mainWindow.textBlockLast.Text = "Last Name:     " + UserConfig.LastName;
                mainWindow.textBlockLogin.Text = "Login:        " + UserConfig.Login;

                List<UserMarks> allRating = new List<UserMarks>();
                allRating.AddRange(_markRepository.GetAllUsersScores());
                if (allRating.Count != 0 && allRating != null)
                {
                    mainWindow.textBlockScore.Text = allRating.Where(f => f.Login == UserConfig.Login).First().Score + " scores";
                    double level = allRating.Where(f => f.Login == UserConfig.Login).First().PercentScore;
                    mainWindow.textBlockPercentScore.Text = allRating.Where(f => f.Login == UserConfig.Login).First().PercentScore + " % success";
                    // Review TK: Please use more meaningfull names.
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();

                    bi3.UriSource = level > 85 ? new Uri("Images/yoda.png", UriKind.Relative) : new Uri("Images/obi-wan-kenobi.png", UriKind.Relative);
                    bi3.EndInit();
                    mainWindow.image.Source = bi3;
                }
                else
                    throw new EmptyTableException();
            }
            catch (EmptyTableException)
            {
                MessageBox.Show("This table is empty for U now, try later");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        ///Tab item FillRating generation
        /// </summary>
        public void FillRating(IMarkRepository _markRepository)
        {
            try
            {
                List<UserMarks> allRating = new List<UserMarks>();
                //Temp test values
                //TODO: Check if test list changed from last query. if changed - reload / else do nothing
                mainWindow.tbRatingMainGrid.Children.Clear();
                mainWindow.tbRatingMainGrid.RowDefinitions.Clear();
                if (mainWindow.Score.IsSelected)
                    allRating.AddRange(_markRepository.GetAllUsersScores().OrderByDescending(c => c.Score).ToList<UserMarks>());
                else
                    allRating.AddRange(_markRepository.GetAllUsersScores().OrderByDescending(c => c.PercentScore).ToList<UserMarks>());
                if (allRating.Count != 0 && allRating != null)
                {
                    int rowCounter = 0;
                    foreach (UserMarks test in allRating)
                    {

                        #region Controls
                        //grid row
                        RowDefinition gridRow = new RowDefinition();
                        gridRow.Height = new GridLength(100);
                        gridRow.Name = "tbCanvasRowTest" + test.Login;

                        mainWindow.tbRatingMainGrid.RowDefinitions.Add(gridRow);

                        //canvas
                        Canvas testCanvas = new Canvas();
                        testCanvas.Name = "canvasTest" + test.Login;
                        testCanvas.Margin = new Thickness(5);
                        testCanvas.Height = 90;
                        Grid.SetRow(testCanvas, rowCounter);
                        Grid.SetColumn(testCanvas, 0);
                        Grid.SetColumnSpan(testCanvas, 2);
                        testCanvas.VerticalAlignment = VerticalAlignment.Top;
                        testCanvas.Background = new SolidColorBrush(Color.FromArgb(255, 0xE0, 0xE9, 0xF5)); //= test.ColorByLevel;
                        testCanvas.Effect = new DropShadowEffect
                        {
                            Color = new Color { R = 0, G = 0, B = 0 },
                            Direction = 320,
                            ShadowDepth = 5,
                            RenderingBias = RenderingBias.Quality,
                            Opacity = 0.6
                        };

                        mainWindow.tbRatingMainGrid.Children.Add(testCanvas);

                        //ellipse
                        Ellipse elipse = new Ellipse();
                        elipse.Name = "iconTest" + test.Login;                        
                        elipse.Fill = RandomColor();
                        elipse.Width = 60;
                        elipse.Height = 60;
                        elipse.VerticalAlignment = VerticalAlignment.Center;
                        elipse.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(elipse, 0);
                        Grid.SetRow(elipse, rowCounter);
                        mainWindow.tbRatingMainGrid.Children.Add(elipse);

                        //ellips textBlock
                        TextBlock textBlock = new TextBlock();
                        textBlock.Name = "iconTextTest" + test.Login;
                        textBlock.FontFamily = new FontFamily("Sevenet 7 Cyr");
                        textBlock.Text = (rowCounter + 1).ToString();
                        textBlock.FontSize = 37;
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.FontWeight = FontWeights.Bold;
                        textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(textBlock, 0);
                        Grid.SetRow(textBlock, rowCounter);
                        mainWindow.tbRatingMainGrid.Children.Add(textBlock);

                        //Inner Grid
                        Grid innerGrid = new Grid();
                        Grid.SetColumn(innerGrid, 1);
                        Grid.SetRow(innerGrid, rowCounter);
                        innerGrid.Name = "tbInnerGridTest" + test.Login;
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextDurationColuntTest" + test.Login });
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextMarkColumnTest" + test.Login });
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextQuestionCountColumnTest" + test.Login });

                        innerGrid.RowDefinitions.Add(new RowDefinition() { Name = "tbTextNameRowTest" + test.Login });
                        mainWindow.tbRatingMainGrid.Children.Add(innerGrid);

                        //AllText
                        //Name TextBlock
                        TextBlock textBlockName = new TextBlock();
                        textBlockName.Name = "textBlockTest" + test.Login + "Name";
                        textBlockName.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockName.Text = test.Login;
                        textBlockName.FontSize = 25;
                        textBlockName.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//black
                        textBlockName.TextWrapping = TextWrapping.Wrap;
                        textBlockName.TextAlignment = TextAlignment.Center;
                        textBlockName.VerticalAlignment = VerticalAlignment.Center;
                        textBlockName.HorizontalAlignment = HorizontalAlignment.Center;

                        Grid.SetColumn(textBlockName, 0);
                        Grid.SetRow(textBlockName, 0);
                        innerGrid.Children.Add(textBlockName);

                        //Description TextBlock
                        TextBlock textBlockDescription = new TextBlock();
                        textBlockDescription.Name = "textBlockTest" + test.Login + "Description";
                        textBlockDescription.Text = test.Score.ToString() + " score";
                        textBlockDescription.FontSize = 30;
                        textBlockDescription.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockDescription.TextWrapping = TextWrapping.Wrap;
                        textBlockDescription.Foreground = new SolidColorBrush(Color.FromRgb(25, 136, 160));
                        textBlockDescription.VerticalAlignment = VerticalAlignment.Center;
                        textBlockDescription.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(textBlockDescription, 1);
                        Grid.SetRow(textBlockDescription, 0);
                        innerGrid.Children.Add(textBlockDescription);

                        TextBlock textBlockDescription2 = new TextBlock();
                        textBlockDescription2.Name = "textBlockTest" + test.Login + "D";
                        textBlockDescription2.Text = test.PercentScore.ToString() + "%";
                        textBlockDescription2.FontSize = 30;
                        textBlockDescription2.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockDescription2.TextWrapping = TextWrapping.Wrap;
                        textBlockDescription2.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//dark gray
                        textBlockDescription2.VerticalAlignment = VerticalAlignment.Center;
                        textBlockDescription2.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(textBlockDescription2, 2);
                        Grid.SetRow(textBlockDescription2, 0);
                        innerGrid.Children.Add(textBlockDescription2);

                        #endregion
                        rowCounter++;
                    }
                }
                else
                    throw new EmptyTableException();
            }
            catch (EmptyTableException e)
            {
                MessageBox.Show("This table is empty for u, try later.  " + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        ///Tab item FillThemeRating generation
        /// </summary>
        public void FillThemeRating(IMarkRepository _markRepository)
        {
            try
            {
                List<UserMarks> allRating = new List<UserMarks>();

                mainWindow.tbThemeRatingMainGrid.Children.Clear();
                mainWindow.tbThemeRatingMainGrid.RowDefinitions.Clear();
                allRating.AddRange(_markRepository.GetAllUsersScores()); 

                if (allRating.Count != 0 && allRating != null )
                {
                    int rowCounter = 0;
                    foreach (UserMarks test in allRating)
                    {
                        if (test.ThemasScore != null && test.ThemasScore.Count != 0)
                        {
                            #region Controls
                            //grid row
                            RowDefinition gridRow = new RowDefinition();
                            gridRow.Height = new GridLength(100);
                            gridRow.Name = "tbaCanvasRowTest" + test.Login;

                            mainWindow.tbThemeRatingMainGrid.RowDefinitions.Add(gridRow);

                            //canvas
                            Canvas testCanvas = new Canvas();
                            testCanvas.Name = "acanvasTest" + test.Login;
                            testCanvas.Margin = new Thickness(5);
                            testCanvas.Height = 90;
                            Grid.SetRow(testCanvas, rowCounter);
                            Grid.SetColumn(testCanvas, 0);
                            Grid.SetColumnSpan(testCanvas, 2);
                            testCanvas.VerticalAlignment = VerticalAlignment.Top;
                            testCanvas.Background = new SolidColorBrush(Color.FromArgb(255, 0xE0, 0xE9, 0xF5)); //= test.ColorByLevel;
                            testCanvas.Effect = new DropShadowEffect
                            {
                                Color = new Color { R = 0, G = 0, B = 0 },
                                Direction = 320,
                                ShadowDepth = 5,
                                RenderingBias = RenderingBias.Quality,
                                Opacity = 0.6
                            };

                            mainWindow.tbThemeRatingMainGrid.Children.Add(testCanvas);

                            //ellipse
                            Ellipse elipse = new Ellipse();
                            elipse.Name = "iconTesti" + test.Login;
                            elipse.Fill = RandomColor();
                            elipse.Width = 40;
                            elipse.Height = 40;
                            elipse.VerticalAlignment = VerticalAlignment.Center;
                            elipse.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(elipse, 0);
                            Grid.SetRow(elipse, rowCounter);
                            mainWindow.tbThemeRatingMainGrid.Children.Add(elipse);

                            //ellips textBlock
                            TextBlock textBlock = new TextBlock();
                            textBlock.Name = "iconTextTesto" + test.Login;
                            textBlock.FontFamily = new FontFamily("Sevenet 7 Cyr");
                            textBlock.Text = (rowCounter + 1).ToString();
                            textBlock.FontSize = 15;
                            textBlock.TextAlignment = TextAlignment.Center;
                            textBlock.FontWeight = FontWeights.Bold;
                            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            textBlock.VerticalAlignment = VerticalAlignment.Center;
                            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(textBlock, 0);
                            Grid.SetRow(textBlock, rowCounter);
                            mainWindow.tbThemeRatingMainGrid.Children.Add(textBlock);

                            //Inner Grid
                            Grid innerGrid = new Grid();
                            Grid.SetColumn(innerGrid, 1);
                            Grid.SetRow(innerGrid, rowCounter);
                            innerGrid.Name = "tbInnerGriadTest" + test.Login;
                            innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextDurationColuntTest" + test.Login , Width=new GridLength(0.7,GridUnitType.Star)});
                            innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextMarkColumnTest" + test.Login });
                            innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextQuestionCountColumnTest" + test.Login });
                            innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextQuestionCountColumnTest2" + test.Login });

                            innerGrid.RowDefinitions.Add(new RowDefinition() { Name = "tbTextNameRowTest" + test.Login });
                            mainWindow.tbThemeRatingMainGrid.Children.Add(innerGrid);

                            //AllText
                            //Name TextBlock
                            TextBlock textBlockName = new TextBlock();
                            textBlockName.Name = "textBlockTest" + test.Login + "Ndame";
                            textBlockName.FontFamily = new FontFamily("Segoe UI Black");
                            textBlockName.Text = test.Login;
                            textBlockName.FontSize = 20;
                            textBlockName.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));
                            textBlockName.TextWrapping = TextWrapping.Wrap;
                            textBlockName.TextAlignment = TextAlignment.Center;
                            textBlockName.VerticalAlignment = VerticalAlignment.Center;
                            textBlockName.HorizontalAlignment = HorizontalAlignment.Center;

                            Grid.SetColumn(textBlockName, 0);
                            Grid.SetRow(textBlockName, 0);
                            innerGrid.Children.Add(textBlockName);

                            //Description TextBlock
                            TextBlock textBlockDescription = new TextBlock();
                            textBlockDescription.Name = "textBlockTest" + test.Login + "fDescription";
                            if (test.ThemasScore.Where(w => w.Theme.Theme == "Grammar").Count() != 0)
                                textBlockDescription.Text = test.ThemasScore.Where(w => w.Theme.Theme == "Grammar").FirstOrDefault().Score.ToString() + "%  Grammar";
                            textBlockDescription.FontSize = 20;
                            textBlockDescription.FontFamily = new FontFamily("Segoe UI Black");
                            textBlockDescription.TextWrapping = TextWrapping.Wrap;
                            textBlockDescription.Foreground = new SolidColorBrush(Color.FromRgb(25, 136, 160));
                            textBlockDescription.VerticalAlignment = VerticalAlignment.Center;
                            textBlockDescription.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(textBlockDescription, 1);
                            Grid.SetRow(textBlockDescription, 0);
                            innerGrid.Children.Add(textBlockDescription);

                            TextBlock textBlockDescription2 = new TextBlock();
                            textBlockDescription2.Name = "textBlockTest" + test.Login + "D";
                            if (test.ThemasScore.Where(w => w.Theme.Theme == "Tenses").Count() != 0)
                                textBlockDescription2.Text = test.ThemasScore.Where(w => w.Theme.Theme == "Tenses").FirstOrDefault().Score.ToString() + "%   Tenses";
                            textBlockDescription2.FontSize = 20;
                            textBlockDescription2.FontFamily = new FontFamily("Segoe UI Black");
                            textBlockDescription2.TextWrapping = TextWrapping.Wrap;
                            textBlockDescription2.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//dark gray
                            textBlockDescription2.VerticalAlignment = VerticalAlignment.Center;
                            textBlockDescription2.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(textBlockDescription2, 2);
                            Grid.SetRow(textBlockDescription2, 0);
                            innerGrid.Children.Add(textBlockDescription2);

                            TextBlock textBlockDescription3 = new TextBlock();
                            textBlockDescription3.Name = "textBlockTest" + test.Login + "gD";
                            if (test.ThemasScore.Where(w => w.Theme.Theme == "Vocabulary").Count() != 0)
                                textBlockDescription3.Text = test.ThemasScore.Where(w => w.Theme.Theme == "Vocabulary").FirstOrDefault().Score.ToString() + "% Vocabulary";
                            textBlockDescription3.FontSize = 20;
                            textBlockDescription3.FontFamily = new FontFamily("Segoe UI Black");
                            textBlockDescription3.TextWrapping = TextWrapping.Wrap;
                            textBlockDescription3.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//dark gray
                            textBlockDescription3.VerticalAlignment = VerticalAlignment.Center;
                            textBlockDescription3.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(textBlockDescription3, 3);
                            Grid.SetRow(textBlockDescription3, 0);
                            innerGrid.Children.Add(textBlockDescription3);

                            #endregion
                            rowCounter++;
                        }
                        
                           
                    }
                }
                else
                    throw new EmptyTableException();
            }
            catch (EmptyTableException e)
            {
                MessageBox.Show("This table is empty for u, try later.  " + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        ///Tab item FillTests generation
        /// </summary>
        public void FillTest(ITestRepository _testRepository)
        {
            try
            {
                List<Test> allTests = new List<Test>();
                allTests.AddRange(_testRepository.GetAllTestForUserWithMarks(UserConfig.Login));

                mainWindow.tbTestMainGrid.Children.Clear();
                mainWindow.tbTestMainGrid.RowDefinitions.Clear();

                if (allTests.Count != 0 && allTests != null)
                {
                    int rowCounter = 0;
                    foreach (Test test in allTests)
                    {

                        #region Controls
                        //grid row
                        RowDefinition gridRow = new RowDefinition();
                        gridRow.Height = new GridLength(160);
                        gridRow.Name = "tbCanvasRowTest" + test.Id;
                        mainWindow.tbTestMainGrid.RowDefinitions.Add(gridRow);

                        //canvas
                        TestCanvas testCanvas = new TestCanvas();
                        testCanvas.Name = "canvasTest" + test.Id;
                        testCanvas.Height = 150;
                        testCanvas.Margin = new Thickness(10, 5, 10, 0);
                        testCanvas.test = test;
                        Grid.SetRow(testCanvas, rowCounter);
                        Grid.SetColumn(testCanvas, 0);
                        Grid.SetColumnSpan(testCanvas, 2);
                        testCanvas.VerticalAlignment = VerticalAlignment.Top;
                        testCanvas.Background = new SolidColorBrush(Color.FromArgb(255, 0xE0, 0xE9, 0xF5)); //= test.ColorByLevel;
                        testCanvas.Effect = new DropShadowEffect
                        {
                            Color = new Color { R = 0, G = 0, B = 0 },
                            Direction = 320,
                            ShadowDepth = 5,
                            RenderingBias = RenderingBias.Quality,
                            Opacity = 0.6
                        };
                        testCanvas.MouseEnter += mainWindow.TestCanvas_MouseEnter;
                        testCanvas.MouseLeave += mainWindow.TestCanvas_MouseLeave;
                        testCanvas.MouseLeftButtonUp += mainWindow.TestCanvas_MouseLeftButtonUp;
                        mainWindow.tbTestMainGrid.Children.Add(testCanvas);

                        //ellipse
                        Ellipse elipse = new Ellipse();
                        elipse.Name = "iconTest" + test.Id;
                        elipse.Width = 120;
                        elipse.Height = 120;
                        elipse.Fill = RandomColor();
                        elipse.VerticalAlignment = VerticalAlignment.Center;
                        elipse.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(elipse, 0);
                        Grid.SetRow(elipse, rowCounter);
                        mainWindow.tbTestMainGrid.Children.Add(elipse);

                        //ellips textBlock
                        TextBlock textBlock = new TextBlock();
                        textBlock.Name = "iconTextTest" + test.Id;
                        textBlock.FontFamily = new FontFamily("Sevenet 7 Cyr");
                        textBlock.Text = test.Name[0].ToString().ToUpper();
                        textBlock.FontSize = 85;
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.FontWeight = FontWeights.Bold;
                        textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(textBlock, 0);
                        Grid.SetRow(textBlock, rowCounter);
                        mainWindow.tbTestMainGrid.Children.Add(textBlock);

                        //Inner Grid
                        Grid innerGrid = new Grid();
                        Grid.SetColumn(innerGrid, 1);
                        Grid.SetRow(innerGrid, rowCounter);
                        innerGrid.Name = "tbInnerGridTest" + test.Id;
                        innerGrid.Margin = new Thickness(5, 5, 5, 5);
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextDurationColuntTest" + test.Id });
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextMarkColumnTest" + test.Id });
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "tbTextQuestionCountColumnTest" + test.Id });
                        innerGrid.RowDefinitions.Add(new RowDefinition() { Name = "tbTextNameRowTest" + test.Id });
                        innerGrid.RowDefinitions.Add(new RowDefinition() { Name = "tbTextDescriptionRowTest" + test.Id });
                        innerGrid.RowDefinitions.Add(new RowDefinition() { Name = "tbTextDurationRowTest" + test.Id });
                        mainWindow.tbTestMainGrid.Children.Add(innerGrid);

                        //AllText
                        //Name TextBlock
                        TextBlock textBlockName = new TextBlock();
                        textBlockName.Name = "textBlockTest" + test.Id + "Name";
                        textBlockName.Text = test.Name + " (" + test.TestLevel.Level + ")";
                        textBlockName.FontSize = 18;
                        textBlockName.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockName.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));
                        textBlockName.TextWrapping = TextWrapping.Wrap;
                        textBlockName.Width = ((innerGrid.Width / 3) * 2) - 10;
                        textBlockName.VerticalAlignment = VerticalAlignment.Center;
                        textBlockName.HorizontalAlignment = HorizontalAlignment.Left;
                        Grid.SetColumnSpan(textBlockName, 2);
                        Grid.SetColumn(textBlockName, 0);
                        Grid.SetRow(textBlockName, 0);
                        innerGrid.Children.Add(textBlockName);

                        //Description TextBlock
                        TextBlock textBlockDescription = new TextBlock();
                        textBlockDescription.Name = "textBlockTest" + test.Id + "Description";
                        textBlockDescription.Text = test.Description;
                        textBlockDescription.FontSize = 16;
                        textBlockDescription.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockDescription.Width = innerGrid.Width - 10;
                        textBlockDescription.TextWrapping = TextWrapping.Wrap;
                        textBlockDescription.Foreground = new SolidColorBrush(Color.FromRgb(50, 50, 50));//dark gray
                        textBlockDescription.VerticalAlignment = VerticalAlignment.Center;
                        textBlockDescription.HorizontalAlignment = HorizontalAlignment.Left;
                        Grid.SetColumnSpan(textBlockDescription, 3);
                        Grid.SetColumn(textBlockDescription, 0);
                        Grid.SetRow(textBlockDescription, 1);
                        innerGrid.Children.Add(textBlockDescription);

                        //Duration TextBlock
                        TextBlock textBlockDuration = new TextBlock();
                        textBlockDuration.Name = "textBlockTest" + test.Id + "Duration";
                        textBlockDuration.Text = test.Duration.Minutes.ToString().PadLeft(2, '0') + ':' + test.Duration.Seconds.ToString().PadLeft(2, '0');
                        textBlockDuration.FontSize = 20;
                        textBlockDuration.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockDuration.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//black
                        textBlockDuration.VerticalAlignment = VerticalAlignment.Center;
                        textBlockDuration.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(textBlockDuration, 0);
                        Grid.SetRow(textBlockDuration, 2);
                        innerGrid.Children.Add(textBlockDuration);

                        if (test.Attempts > 0)
                        {
                            //Mark TextBlock
                            TextBlock textBlockMark = new TextBlock();
                            textBlockMark.Name = "textBlockTest" + test.Id + "Mark";
                            textBlockMark.Text = test.MaxPercent.ToString() + "%";
                            textBlockMark.FontSize = 20;
                            textBlockMark.FontFamily = new FontFamily("Segoe UI Black");
                            textBlockMark.Foreground = test.MaxPercent >= test.PassValue ? new SolidColorBrush(Color.FromRgb(0, 255, 0)) : new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            textBlockMark.VerticalAlignment = VerticalAlignment.Center;
                            textBlockMark.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(textBlockMark, 1);
                            Grid.SetRow(textBlockMark, 2);
                            innerGrid.Children.Add(textBlockMark);

                            //Attemps TextBlock
                            TextBlock textBlockAttemps = new TextBlock();
                            textBlockMark.Name = "textBlockTest" + test.Id + "Attempts";
                            textBlockAttemps.Text = test.Attempts > 1 ? test.Attempts.ToString() + " attempts" : test.Attempts.ToString() + " attempt";
                            textBlockAttemps.FontSize = 16;
                            textBlockAttemps.FontFamily = new FontFamily("Segoe UI Black");
                            textBlockAttemps.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//black
                            textBlockAttemps.VerticalAlignment = VerticalAlignment.Center;
                            textBlockAttemps.HorizontalAlignment = HorizontalAlignment.Center;
                            Grid.SetColumn(textBlockAttemps, 2);
                            Grid.SetRow(textBlockAttemps, 2);
                            innerGrid.Children.Add(textBlockAttemps);
                        }
                        //QuestionCount TextBlock
                        TextBlock textBlockQuestionCount = new TextBlock();
                        textBlockQuestionCount.Name = "textBlockTest" + test.Id + "QuestionCount";
                        textBlockQuestionCount.Text = test.QuestionsCount > 1 ? test.QuestionsCount.ToString() + " question" : "No questions!";
                        textBlockQuestionCount.FontSize = 14;
                        textBlockQuestionCount.FontFamily = new FontFamily("Segoe UI Black");
                        textBlockQuestionCount.Foreground = new SolidColorBrush(Color.FromRgb(13, 74, 135));//black
                        textBlockQuestionCount.Width = ((innerGrid.Width / 3) * 2) - 10;
                        textBlockQuestionCount.VerticalAlignment = VerticalAlignment.Center;
                        textBlockQuestionCount.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(textBlockQuestionCount, 2);
                        Grid.SetRow(textBlockQuestionCount, 0);
                        innerGrid.Children.Add(textBlockQuestionCount);

                        #endregion
                        rowCounter++;
                    }
                }
                else
                    throw new EmptyTableException();
            }
            catch (EmptyTableException e)
            {
                MessageBox.Show("This table is empty for u, try later.  " + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
       
        private Brush RandomColor()
        {
            return new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));
        }        

        public void StartGenerateTest(TestTabItem testTab)
        {
            mainWindow.mainTabControl.SelectedItem = testTab;
            if (!testTab.isTestStarted)
            {
                Generate(testTab);
            }
        }        
        private void Generate(TestTabItem testTabItem)
        {
            if (!testTabItem.isTestStarted)
            {
                Test test = testTabItem.test;
                Grid testStartGrid = new Grid()
                {
                    Name = "tbStartTest" + test.Id,
                    Margin = new Thickness(50, 10, 50, 150)
                };
                ColumnDefinitionCollection cDefs = testStartGrid.ColumnDefinitions;
                cDefs.Add(new ColumnDefinition() { Name = "tbTest" + test.Id + "StartLeftColumn" });
                cDefs.Add(new ColumnDefinition() { Name = "tbTest" + test.Id + "StartCenterColumn" });
                cDefs.Add(new ColumnDefinition() { Name = "tbTest" + test.Id + "StartRightColumn" });

                RowDefinitionCollection rDefs = testStartGrid.RowDefinitions;
                rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "StartNameRow" });
                rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "StartDescriptionRow" });
                rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "StartQuestionCountRow" });
                rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "StartOtherInfoRow" });
                rDefs.Add(new RowDefinition() { Name = "tbTest" + test.Id + "StartButtonsRow" });
                testTabItem.Content = testStartGrid;

                //Name TextBlock
                TextBlock textBlockName = new TextBlock()
                {
                    Name = "textBlockTest" + test.Id + "StartName",
                    Text = test.Name.ToUpper(),
                    FontSize = 40,

                    Foreground = new SolidColorBrush(Color.FromArgb(255, 52, 216, 233)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetColumnSpan(textBlockName, 3);
                Grid.SetColumn(textBlockName, 0);
                Grid.SetRow(textBlockName, 0);
                testStartGrid.Children.Add(textBlockName);

                //Description TextBlock
                TextBlock textBlockDescription = new TextBlock()
                {
                    Name = "textBlockTest" + test.Id + "StartDescription",
                    Text = test.Description,
                    FontSize = 25,
                    FontFamily = new FontFamily("Franklin Gothic Medium"),
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 94, 219, 239)),
                    TextWrapping = TextWrapping.Wrap,
                    Width = (testStartGrid.Width) - 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetColumnSpan(textBlockDescription, 3);
                Grid.SetColumn(textBlockDescription, 0);
                Grid.SetRow(textBlockDescription, 1);
                testStartGrid.Children.Add(textBlockDescription);

                //QuestionCount TextBlock
                TextBlock textBlockQuestionCount = new TextBlock()
                {
                    Name = "textBlockTest" + test.Id + "StartQuestionCount",
                    Text = test.QuestionsCount > 1 ? test.QuestionsCount.ToString() + " quest." : "No questions!",
                    FontSize = 25,
                    FontFamily = new FontFamily("Franklin Gothic Medium"),
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 181, 217, 230)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetColumnSpan(textBlockQuestionCount, 3);
                Grid.SetColumn(textBlockQuestionCount, 0);
                Grid.SetRow(textBlockQuestionCount, 2);
                testStartGrid.Children.Add(textBlockQuestionCount);

                //Duration TextBlock
                TextBlock textBlockDuration = new TextBlock()
                {
                    Name = "textBlockTest" + test.Id + "StartDuration",
                    Text = test.Duration.Minutes.ToString().PadLeft(2, '0') + ':' + test.Duration.Seconds.ToString().PadLeft(2, '0'),
                    FontSize = 25,
                    FontFamily = new FontFamily("Franklin Gothic Medium"),
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 181, 217, 230)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetColumn(textBlockDuration, 0);
                Grid.SetRow(textBlockDuration, 3);
                testStartGrid.Children.Add(textBlockDuration);

                if (test.Attempts > 0)
                {
                    //Mark TextBlock
                    TextBlock textBlockMark = new TextBlock()
                    {
                        Name = "textBlockTest" + test.Id + "StartMark",
                        Text = test.MaxPercent.ToString() + "%",
                        FontSize = 25,
                        Foreground = test.MaxPercent >= test.PassValue ? new SolidColorBrush(Color.FromRgb(40, 197, 16)) : new SolidColorBrush(Color.FromRgb(209, 10, 10)),
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Franklin Gothic Medium"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };
                    Grid.SetColumn(textBlockMark, 1);
                    Grid.SetRow(textBlockMark, 3);
                    testStartGrid.Children.Add(textBlockMark);

                    //Attemps TextBlock
                    TextBlock textBlockAttemps = new TextBlock()
                    {
                        Name = "textBlockTest" + test.Id + "StartAttempts",
                        Text = test.Attempts > 1 ? test.Attempts.ToString() + " tries" : test.Attempts.ToString() + " tries",
                        FontSize = 25,
                        FontFamily = new FontFamily("Franklin Gothic Medium"),
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 181, 217, 230)),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };
                    Grid.SetColumn(textBlockAttemps, 2);
                    Grid.SetRow(textBlockAttemps, 3);
                    testStartGrid.Children.Add(textBlockAttemps);

                }
                Button testCloseButton = new Button()
                {
                    Style = mainWindow.FindResource("ButtonStyle") as Style,
                    Content = "Close"
                };
                testCloseButton.Click += mainWindow.TestCloseButton_Click;

                Grid.SetColumn(testCloseButton, 0);
                Grid.SetRow(testCloseButton, 4);
                testStartGrid.Children.Add(testCloseButton);

                Button testStartButton = new Button()
                {
                    Style = mainWindow.FindResource("ButtonStyle") as Style,
                    Content = "Start"
                };

                if (test.QuestionsCount != 0 && test.Questions.ConvertAll<int>(c => c.Answers.Count).Sum() != 0)
                {
                    testStartButton.Click += mainWindow.TestStartButton_Click;
                }
                else
                {
                    testStartButton.IsEnabled = false;
                }

                Grid.SetColumn(testStartButton, 3);
                Grid.SetRow(testStartButton, 4);
                testStartGrid.Children.Add(testStartButton);
            }
        }
    }  
}
