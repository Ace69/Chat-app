using Communication.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Communication.App
{
    public static class LoggedUserID
    {
        public static string LoggedUserMail;
        public static UserModel userModel;
        public static GroupModel actualGroupModel;
        public static ContributionModel actualContributionModel;
        public static Window mainWindow;
        public static UserModel selectedUserModel;
    }
}
