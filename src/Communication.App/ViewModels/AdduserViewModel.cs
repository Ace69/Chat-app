using Communication.App.Commands;
using Communication.BL.Messages;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.DAL.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
namespace Communication.App.ViewModels
{
    public class AdduserViewModel : ViewModelBase
    {
        private readonly GroupMemberRepository groupMemberRepository;
        private readonly UserRepository userRepository;
        private readonly IMediator mediator;
        public ICommand AddUserToGroupCommand { get; set; }

        public string addedUserEmail { get; set; }
        public GroupMemberModel _groupMemberModel = new GroupMemberModel();
        public GroupMemberModel groupMemberModel
        {
            get
            {
                return _groupMemberModel;
            }
            set
            {
                if (_groupMemberModel != value)
                {
                    _groupMemberModel = value;
                    OnPropertyChanged("groupMemberModel");
                }
            }

        }

        public AdduserViewModel(GroupMemberRepository groupMemberRepository, UserRepository userRepository, IMediator mediator)
        {
            this.groupMemberRepository = groupMemberRepository;
            this.userRepository = userRepository;
            this.mediator = mediator;
            this.AddUserToGroupCommand = new RelayCommand(AddUserToGroupFunction);
        }

        public void AddUserToGroupFunction() 
        {
            groupMemberModel.Id = Guid.NewGuid();
            groupMemberRepository.Insert(groupMemberModel);
            var user = userRepository.GetUserByEmail(this.addedUserEmail);
            groupMemberModel.User = user;
            groupMemberModel.Group = LoggedUserID.actualGroupModel;
            groupMemberRepository.dbContext.RemoveEntities<GroupEntity>();
            groupMemberRepository.dbContext.RemoveEntities<UserEntity>();
            groupMemberRepository.Update(groupMemberModel);
            mediator.Send(new AddedMemberMessage());
            addedUserEmail = string.Empty;
            OnPropertyChanged("groupMemberModel");
        }
    }
}
