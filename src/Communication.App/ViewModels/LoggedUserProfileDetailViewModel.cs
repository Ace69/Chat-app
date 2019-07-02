using Communication.BL.Messages;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Communication.App.Commands;

namespace Communication.App.ViewModels
{
    public class LoggedUserProfileDetailViewModel : ViewModelBase
    {
        private readonly UserRepository userRepository;
        private readonly IMediator mediator;
        public ICommand GoBack { get; set; }

        public UserModel _Model;
        public UserModel Model
        {
            get
            {
                return _Model;
            }
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        public LoggedUserProfileDetailViewModel(UserRepository userRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.mediator = mediator;

            GoBack = new RelayCommand(Cancel);
            mediator.Register<OpenUserProfileMessage>(ShowWindowProfile);
            mediator.Register<CloseLoggedUserProfile>(CloseWindow);
        }

        private void CloseWindow(CloseLoggedUserProfile obj)
        {
            Model = null;
        }

        private void ShowWindowProfile(OpenUserProfileMessage obj) 
        {
            Model = new UserModel();
            mediator.Send(new CloseRegistrationGroupWindow());
            mediator.Send(new CloseContributionsWindowMessage());
            mediator.Send(new CloseCommentsWindowMessage());
            mediator.Send(new CloseSearchUserMessage());
            Load(LoggedUserID.LoggedUserMail);
        }

        public void Load(string LoggedUser)
        {
            Model = userRepository.GetUserByEmail(LoggedUser);
        }
        public void Cancel()
        {
            Model = null;
        }
    }
}
