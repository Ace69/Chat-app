
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.BL.Models;
using System.Collections.Generic;

namespace Communication.App.ViewModels

{
    public class LoggedUserViewModel : ViewModelBase
    {
        private readonly UserRepository userRepository;
        private readonly GroupRepository groupRepository;
        private readonly IMediator mediator;

        public UserModel _Model = new UserModel();
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

        public LoggedUserViewModel(UserRepository userRepository, GroupRepository groupRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
            this.mediator = mediator;
            Load(LoggedUserID.LoggedUserMail);
        }

        public void Load(string LoggedUser)
        {
            Model = userRepository.GetUserByEmail(LoggedUser);
            LoggedUserID.userModel = Model;

            var groups = groupRepository.GetAllGroupsByEmail(Model.Email);
            List<GroupModel> groupsList = (List<GroupModel>)groups;
            if (groupsList.Count > 0)
            {
                LoggedUserID.actualGroupModel = groupsList[0];
            }
        }
    }
}
