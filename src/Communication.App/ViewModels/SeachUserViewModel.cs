using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.BL.Models;
using Communication.BL.Messages;
using Communication.App.Commands;
using System.Windows.Input;

namespace Communication.App.ViewModels
{
    public class SeachUserViewModel : ViewModelBase
    {
        private readonly UserRepository userRepository;
        private readonly IMediator mediator;

        public ICommand LoadUser { get; set; }
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

        public SeachUserViewModel(UserRepository userRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.mediator = mediator;
            LoadUser = new RelayCommand(LoadUserExecute);
            GoBack = new RelayCommand(Cancel);
            mediator.Register<OpenSearchUserMessage>(OpenWindow);
            mediator.Register<CloseSearchUserMessage>(CloseWindow);
        }

        public void LoadUserExecute()
        {
            if (userRepository.GetUserByEmail(Model.Email) != null)
            {
                Model = userRepository.GetUserByEmail(Model.Email);
            }
        }

        public void Cancel()
        {
            Model = null;
        }

        public void OpenWindow(OpenSearchUserMessage obj)
        {
            Model = new UserModel();
            mediator.Send(new CloseCommentsWindowMessage());
            mediator.Send(new CloseContributionsWindowMessage());
            mediator.Send(new CloseLoggedUserProfile());
            mediator.Send(new CloseRegistrationGroupWindow());
        }
        public void CloseWindow(CloseSearchUserMessage obj)
        {
            Model = null;
        }
    }
}
