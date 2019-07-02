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
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Communication.BL.Models;
using Communication.BL.Services;
using Communication.BL.Exceptions;

namespace Communication.App
{
    /// <summary>
    /// Interakční logika pro RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void BackToLogginButton_Click(object sender, RoutedEventArgs e)
        {
            LogginWindow loggin = new LogginWindow();
            loggin.Show();
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private bool RegisterNewuser(string email, string name, string surname, string telNumber, string password)
        {
            LoginService loginService = new LoginService();
            try
            {
                loginService.register(name, surname, email, password, telNumber);
            }catch(CommunicationException ex)
            {
                ErrorMessage.Text = ex.Message;
                return false;
            }
            return true;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = ""; // vyynulování Error message pokud opravim data a chci se registrovat 
            OKMessage.Text = "";

            string email = MailTextBox.Text;
            string name = JmenoTextBox.Text;
            string surname = PrijmeniTextBox.Text;
            string tel_number = TelTextBox.Text;

            string password = heslo1PasswordBox.Password;
            string password_again = heslo2PasswordBox.Password;


            if (name.Length < 1)
            {
                ErrorMessage.Text = "Jméno uživatele není zadáno";
                JmenoTextBox.Focus();
                return;
            }

            if (surname.Length < 1)
            {
                ErrorMessage.Text = "Příjmení uživatele není zadáno";
                PrijmeniTextBox.Focus();
                return;
            }

            if (email.Length < 1)
            {
                ErrorMessage.Text = "Emailová adresa není zadána";
                MailTextBox.Focus();
                return;
            }
            if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                ErrorMessage.Text = "Nebyl zadán platný email";
                MailTextBox.Focus();
                return;
            }

            if (tel_number.Length < 1)
            {
                ErrorMessage.Text = "Telefonní číslo uživatele není zadáno";
                TelTextBox.Focus();
                return;
            }

            if (password.Length < 1)
            {
                ErrorMessage.Text = "Nebylo zadáno heslo";
                heslo1PasswordBox.Focus();
                return;
            }

            if (password_again.Length < 1)
            {
                ErrorMessage.Text = "Nutné zadat heslo 2x pro ověření";
                heslo2PasswordBox.Focus();
                return;
            }

            if (password.Length < 6)
            {
                ErrorMessage.Text = "Heslo musí obsahovat alespoň 6 znaků";
                heslo1PasswordBox.Focus();
                return;
            }
            if (password != password_again)
            {
                ErrorMessage.Text = "Zadaná hesla nejsou stejná";
                heslo2PasswordBox.Focus();
                return;
            }

            if (RegisterNewuser(email, name, surname, tel_number, password))
            {
                OKMessage.Text = "Registrace proběhla úspěšně";
                return;
            }
            //}
        }

        public void Reset()
        {
            JmenoTextBox.Text = "";
            PrijmeniTextBox.Text = "";
            MailTextBox.Text = "";
            TelTextBox.Text = "";
            heslo1PasswordBox.Password = "";
            heslo2PasswordBox.Password = "";
            ErrorMessage.Text = "";
        }
    }
}
