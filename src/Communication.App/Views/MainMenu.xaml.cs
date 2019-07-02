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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Communication.App.Views
{
    /// <summary>
    /// Interakční logika pro MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogginWindow logginWindow = new LogginWindow();
            logginWindow.Show();
            LoggedUserID.mainWindow.Close();
            LoggedUserID.userModel = null;
            LoggedUserID.actualGroupModel = null;
            LoggedUserID.actualContributionModel = null;
            LoggedUserID.LoggedUserMail = null;
        }
    }
}
