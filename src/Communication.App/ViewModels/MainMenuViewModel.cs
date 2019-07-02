using System;
using System.Windows.Input;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.BL.Models;
using Communication.BL.Messages;
using Communication.App.Commands;
using System.Windows;

namespace Communication.App.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        public ICommand OpenMyProfile{ get; set; }
        public ICommand SearchUser { get; set; }

        public RelayCommand<Window> LogoutUserCommand { get; set; }

        public UserRepository userRepository;

        public MainMenuViewModel(IMediator mediator)
        {
            this.mediator = mediator;
            OpenMyProfile = new RelayCommand(SearchMyProfile);
            SearchUser = new RelayCommand(GoSearchUser);
            LogoutUserCommand = new RelayCommand<Window>(Logout);
        }

        public void SearchMyProfile()
        {
            mediator.Send(new OpenUserProfileMessage());
        }

        public void GoSearchUser()
        {
            mediator.Send(new OpenSearchUserMessage());
        }

        public void Logout(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
