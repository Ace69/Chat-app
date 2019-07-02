using System;
using System.Windows.Input;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.BL.Models;
using Communication.BL.Messages;
using Communication.App.Commands;
using Communication.DAL.Entities;
using System.Linq;

namespace Communication.App.ViewModels
{
    public class NewGroupViewModel : ViewModelBase
    {
        private readonly GroupRepository groupRepository;
        private GroupMemberRepository groupMemberRepository;
        private readonly IMediator mediator;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public GroupModel _Model;
        public GroupModel Model
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


        public NewGroupViewModel(GroupRepository groupRepository, GroupMemberRepository groupMemberRepository, IMediator mediator)
        {
            this.groupRepository = groupRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.mediator = mediator;
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);

            mediator.Register<ShowGroupRegistration>(ShowWindow);
            mediator.Register<CloseRegistrationGroupWindow>(CloseWindow);
        }

        private void ShowWindow(ShowGroupRegistration obj) // msg type
        {
            mediator.Send(new CloseLoggedUserProfile());
            mediator.Send(new CloseContributionsWindowMessage());
            mediator.Send(new CloseSearchUserMessage());
            mediator.Send(new CloseCommentsWindowMessage());
            Model = new GroupModel();
        }

        private void CloseWindow(CloseRegistrationGroupWindow obj)
        {
            Model = null;
        }

        public void Cancel()
        {
            Model = null;
        }

        public void Save()
        {
            Model.Id = Guid.NewGuid();
            GroupMemberModel groupMember = new GroupMemberModel();
            groupMember.Id = Guid.NewGuid();
            groupMember.Group = Model;
            groupMemberRepository.Insert(groupMember);

            var groupMemberGet = groupMemberRepository.dbContext.GroupMembers.SingleOrDefault(i => i.Id == groupMember.Id);

            groupMemberGet.User = groupMemberRepository.dbContext.Users.SingleOrDefault(i => i.Id == LoggedUserID.userModel.Id);

            groupMemberRepository.dbContext.SaveChanges();


            mediator.Send(new AddedGroupMessage { Id = Model.Id });
            Model = null;
        }
    }
}
