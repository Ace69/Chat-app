using Communication.App.Commands;
using Communication.BL.Messages;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.BL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Communication.App.ViewModels
{
    public class AddOrDeleteGroupMemeberViewModel : ViewModelBase
    {
        private readonly UserRepository userRepository;
        private readonly GroupRepository groupRepository;
        private readonly GroupMemberRepository groupMemberRepository;
        private readonly IMediator mediator;

        public ObservableCollection<UserModel> Members { get; set; } = new ObservableCollection<UserModel>();

        public ICommand UserSelectedCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        public AddOrDeleteGroupMemeberViewModel(UserRepository userRepository, GroupRepository groupRepository, GroupMemberRepository groupMemberRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.mediator = mediator;

            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            UserSelectedCommand = new RelayCommand<UserModel>(UserSelected);

            Load();
        }

        private void AddUser()
        {
            GroupMemberModel groupMember = new GroupMemberModel();

            groupMember.Id = Guid.NewGuid();
            groupMember.User.Id = userRepository.GetById(LoggedUserID.selectedUserModel.Id).Id;
            groupMember.Group.Id = groupRepository.GetById(LoggedUserID.actualGroupModel.Id).Id;
            groupMember.Permission = DAL.Enums.PermissionEnum.User;

            groupMemberRepository.Insert(groupMember);
        }

        private void DeleteUser(object obj)
        {
            GroupMemberModel groupMember = groupMemberRepository.GetGroupMemberByIDS(LoggedUserID.selectedUserModel.Id, LoggedUserID.selectedUserModel.Id).FirstOrDefault();
            groupMemberRepository.Delete(groupMember);
        }

        private void UserSelected(UserModel user)
        {
            LoggedUserID.selectedUserModel = user;
            mediator.Send(new SelectedUserMessage { Id = user.Id });
        }

        public void Load()
        {
            Members.Clear();

            //var mem = userRepository.GetAllUsersOfGroupByGroupId(LoggedUserID.actualGroupModel.Id);
            //foreach (var item in mem)
            //{
            //    Members.Add(item);
            //}
        }
    }
}
