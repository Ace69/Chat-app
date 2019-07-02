using System.Collections.ObjectModel;
using System.Windows.Input;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.BL.Models;
using Communication.BL.Messages;
using Communication.App.Commands;
using System.Collections.Generic;

namespace Communication.App.ViewModels
{
    public class GroupListViewModel : ViewModelBase
    {
        private readonly GroupRepository groupRepository;
        private readonly IMediator mediator;

        public ObservableCollection<GroupModel> Groups { get; set; } = new ObservableCollection<GroupModel>();

        public ICommand GroupSelectedCommand { get; set; }
        public ICommand GroupNewCommand { get; set; }
        public ICommand AddNewGroupCommandVisibly { get; set; }

        public GroupListViewModel(GroupRepository groupRepository, IMediator mediator)
        {
            this.groupRepository = groupRepository;
            this.mediator = mediator;

            GroupNewCommand = new RelayCommand(GroupNew);
            GroupSelectedCommand = new RelayCommand<GroupModel>(GroupSelected);
            AddNewGroupCommandVisibly = new RelayCommand(Show);

            mediator.Register<AddedGroupMessage>(GroupAdded);
            Load();
        }

        private void GroupNew() => mediator.Send(new NewGroupMessage());

        private void GroupSelected(GroupModel group) {
            LoggedUserID.actualGroupModel = group;
            mediator.Send(new SelectedGroupMessage { Id = group.Id });
        }
        private void GroupUpdated(UpdatedGroupMessage group) => Load();
        private void GroupAdded(AddedGroupMessage group) => Load();
        private void GroupDeleted(DeletedGroupMessage group) => Load();
        public void Load()
        {
            Groups.Clear();
            string loggedUserEmail = LoggedUserID.LoggedUserMail;
            var groups = groupRepository.GetAllGroupsByEmail(loggedUserEmail);

            foreach (var item in groups)
            {
                Groups.Add(item);
            }
        }

        public void Show()
        {
            mediator.Send(new ShowGroupRegistration()); // odeslani zpravy pri zmakcnuti tlacitka
        }

    }
}
