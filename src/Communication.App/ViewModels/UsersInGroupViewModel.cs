using System.Collections.ObjectModel;
using System.Windows.Input;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.BL.Models;
using Communication.BL.Messages;
using Communication.App.Commands;
using System.Collections.Generic;
using System;

namespace Communication.App.ViewModels
{
    public class UsersInGroupViewModel : ViewModelBase
    {
        private readonly UserRepository userRepository;
        private readonly IMediator mediator;

        public UserModel _userModel;
        public UserModel userModel
        {
            get
            {
                return _userModel;
            }
            set
            {
                if (_userModel != value)
                {
                    _userModel = value;
                    OnPropertyChanged("userModel");
                }
            }

        }

        public ObservableCollection<UserModel> UsersInGroup { get; set; } = new ObservableCollection<UserModel>();

        public UsersInGroupViewModel(UserRepository userRepository,IMediator mediator)
        {
            this.userRepository = userRepository;
            this.mediator = mediator;

            mediator.Register<ShowUsersInGroupMessage>(ShowUsersInGroupFunction);
            mediator.Register<AddedMemberMessage>(Refresh); // 
            // Mediator bude očekáváat zpravý typy user in group added/deleted 
        }

        public void ShowUsersInGroupFunction(ShowUsersInGroupMessage obj)
        {
            Load();
        }

        public void Refresh(AddedMemberMessage obj)
        {
            Load(); 
        }

        public void Load()
        {
            UsersInGroup.Clear();
                        
             Guid idSkupiny = LoggedUserID.actualGroupModel.Id;
            var usersInGroup = userRepository.GetAllUsersOfGroupByGroupId(idSkupiny);

            foreach (var item in usersInGroup)
            {
               UsersInGroup.Add(item);
            }
        }
    }
}
