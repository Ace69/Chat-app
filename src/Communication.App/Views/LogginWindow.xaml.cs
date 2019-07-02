using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Communication.BL.Exceptions;
using Communication.BL.Models;
using Communication.BL.Services;

namespace Communication.App
{
    /// <summary>
    /// Interakční logika pro LogginWindow.xaml
    /// </summary>
    public partial class LogginWindow : Window
    {
        public LogginWindow()
        {
            InitializeComponent();
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration= new RegistrationWindow();
            registration.Show();
            Close();
        }

        private bool AutentizationVerify(string Username, string Password)
        {
            LoginService loginService = new LoginService();
            try
            {
                UserModel verifyModel = loginService.LoadUserByEmail(Username, Password);
            }catch(LoginException ex)
            {
                ErrorMessage.Text = ex.Message;
                return false;
            }
            return true;
        }

        private void LogginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";

            string UserMail = MailTextBox.Text;
            string userPassword = PasswordBox.Password;


            if (userPassword.Length == 0)
            {
                ErrorMessage.Text = "Nebylo zadáno heslo";
                PasswordBox.Focus();
                return;
            }

            if (UserMail.Length == 0)
            {
                ErrorMessage.Text = "Nebyl zadán přihlašovací email";
                MailTextBox.Focus();
                return;
            }
            if(AutentizationVerify(UserMail,userPassword))
            {
                LoggedUserID.LoggedUserMail = UserMail; //  Který uživatel je přihlášený
                MainWindow mainWindow = new MainWindow(); 
                mainWindow.Show();
                LoggedUserID.mainWindow = mainWindow;
                Close();
            }
        }

        private void KeyBinding_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrationButton_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
