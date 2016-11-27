using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.DAL.Concrete.Repository;
using EnglishGrammar.Entities;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;


namespace WpfAppEnglishGrammar
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        // Review TK: It is a good practice to make such initializartion within constructor.
        IAppUserRepository _appUserRepository = new AppUserRepository(ConfigurationManager.ConnectionStrings["EnglishJediConnection"].ConnectionString);
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            // Review TK: You could use more appropriate name for boolean variables. For instance, IsValid or something like that.
            if (Validate)
            {
                AppUser newUser = new AppUser()
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Password
                };
                _appUserRepository.SetNewUser(newUser);
                MessageBox.Show("Your registration was successful. Now you can login");
                this.Close();
            }
        }
        // Review TK: I don't see any access modifiers.
        bool Validate
        {
            get
            {
                return IsPasswordsMatch && IsPasswordsNotSmall && IsLoginAllowed && IsLastNameFiled && IsFirstNameFiled;
            }
        }
        bool IsPasswordsMatch
        {
            get
            {
                if (tbPassword.Password == tbPasswordConfirm.Password)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Passwords don't match.");
                    return false;
                }
            }
        }
        bool IsLastNameFiled
        {
            get
            {
                if (!string.IsNullOrEmpty(tbLastName.Text))
                {
                    return true;
                }
                // Review TK: Please try to avoid redundant else statement.
                else
                {
                    MessageBox.Show("LastName could not be empty.");
                    return false;
                }
            }
        }
        bool IsLoginFiled
        {
            get
            {
                if (!string.IsNullOrEmpty(tbLogin.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Login could not be empty.");
                    return false;
                }
            }
        }
        bool IsFirstNameFiled
        {
            get
            {
                if (!string.IsNullOrEmpty(tbFirstName.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("FirstName could not be empty.");
                    return false;
                }
            }
        }
        bool IsPasswordsNotSmall
        {
            get
            {
                if (tbPassword.Password.Length > 3)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Passwords is too small. Try enother");
                    return false;
                }
            }
        }
        bool IsLoginAllowed
        {
            get
            {
                if (_appUserRepository.IsUserAlowed(tbLogin.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Login is occupied. Try enother");
                    return false;
                }
            }
        }

        // Review TK: It seems you don't use this methods.
        private void showPasswordConfirm_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                tbPasswordConfirm.Visibility = Visibility.Visible;
            }
            {
                tbPasswordConfirm.Visibility = Visibility.Hidden;
            }
        }

        private void showPassword_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                tbPassword.Visibility = Visibility.Visible;
            }
            {
                tbPassword.Visibility = Visibility.Hidden;
            }
        }

        private void registerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
