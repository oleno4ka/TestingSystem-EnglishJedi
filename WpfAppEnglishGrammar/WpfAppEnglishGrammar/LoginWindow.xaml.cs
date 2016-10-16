using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Configuration;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.DAL.Concrete.Repository;
using EnglishGrammar.Entities;

namespace WpfAppEnglishGrammar
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Private Fields
        private readonly IAppUserRepository _appUserRepository;
        #endregion
        #region Constructor
        public LoginWindow()
        {
            _appUserRepository = new AppUserRepository(ConfigurationManager.ConnectionStrings["EnglishJediConnection"].ConnectionString);
            InitializeComponent();
        }
        #endregion
        #region EventMethods
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registrationWindow = new RegisterWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login =  tbLogin.Text;
            string password =  tbPassword.Password;
            AppUser user = _appUserRepository.GetUserByLogin(login, password);

            if (user == null)
            {
                MessageBox.Show(this, "Invalid user name or password", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserConfig.Initialize(user);
                // this.DialogResult = DialogResult.OK;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
        }

        private void tbLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            tbLogin.Foreground = Brushes.Azure;
        }

        private void tbPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            tbPassword.Foreground = Brushes.Azure;
        }
        #endregion
    }
}
