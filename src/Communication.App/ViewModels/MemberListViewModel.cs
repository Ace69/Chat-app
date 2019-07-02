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

namespace Communication.App.ViewModels
{
    public class MemberListViewModel : ViewModelBase
    {
        private readonly UserRepository userRepository;
        private readonly IMediator mediator;

        public ObservableCollection<UserModel> Members { get; set; } = new ObservableCollection<UserModel>();

        public UserModel _userModel = new UserModel();
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

        public MemberListViewModel(UserRepository userRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.mediator = mediator;
            mediator.Register<AddedMemberMessage>(MemberAdded);
            LoadMembers();
        }

        private void MemberAdded(AddedMemberMessage obj)
        {
            LoadMembers();
        }

        public void LoadMembers()
        {
            Members.Clear();
            var mem = userRepository.GetAllUsersOfGroupByGroupId(LoggedUserID.actualGroupModel.Id);
            foreach (var item in mem)
            {
                Members.Add(item);
            }
        }
    }
}
