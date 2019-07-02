using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.App.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Communication.BL.Messages;
using Communication.DAL.Entities;

namespace Communication.App.ViewModels
{
    public class ContributionViewModel : ViewModelBase
    {
        private readonly ContributionRepository contributionRepository;
        private readonly CommentRepository commentRepository;
        private readonly IMediator mediator;
        public string loggedUserEmail;

        public ObservableCollection<ContributionModel> Contributions { get; set; } = new ObservableCollection<ContributionModel>();

        public ICommand ContributionNewCommand { get; set; }

        public ICommand ContributionSelectedCommand { get; set; }

        public ICommand CommentCloseCommand { get; set; }

        public ContributionModel _contributionModel;
        public ContributionModel contributionModel
        {
            get
            {
                return _contributionModel;
            }
            set
            {
                if (_contributionModel != value)
                {
                    _contributionModel = value;
                    OnPropertyChanged("contributionModel");
                }
            }
        }

        public ContributionViewModel(ContributionRepository contributionRepository,CommentRepository commentRepository, IMediator mediator)
        {
            this.contributionRepository = contributionRepository;
            this.commentRepository = commentRepository;
            this.mediator = mediator;
            this.loggedUserEmail = LoggedUserID.LoggedUserMail;

            ContributionNewCommand = new RelayCommand(ContributionNew);
            ContributionSelectedCommand = new RelayCommand<ContributionModel>(ContributionSelected);
            CommentCloseCommand = new RelayCommand(CommentClose);

            mediator.Register<NewContributionMessage>(NewContribution);
            mediator.Register<SelectedGroupMessage>(ShowContributionsWindow);
            mediator.Register<CloseContributionsWindowMessage>(CloseWindow);
            mediator.Register<NewCommentMessage>(NewComment);
            LoadContributions();
        }

        private void ShowContributionsWindow(SelectedGroupMessage obj)
        {
            mediator.Send(new CloseRegistrationGroupWindow());
            mediator.Send(new CloseLoggedUserProfile());
            mediator.Send(new CloseSearchUserMessage());
            mediator.Send(new CloseCommentsWindowMessage());
            contributionModel = new ContributionModel();
            mediator.Send(new ShowUsersInGroupMessage());
            LoadContributions();
        }


        private void CloseWindow(CloseContributionsWindowMessage obj)
        {
            contributionModel = null;
        }

        private void NewComment(NewCommentMessage obj) => LoadContributions();

        private void NewContribution(NewContributionMessage contribution) => LoadContributions();
        private void ContributionSelected(ContributionModel contribution)
        {
            LoggedUserID.actualContributionModel = contribution;
            mediator.Send(new ContributionSelected { Id = contribution.Id });
            contributionModel = null;
        }

        private void CommentClose()
        {
            mediator.Send(new CloseCommentsWindowMessage());
        }

        private bool CheckModel(ContributionModel contributionModel)
        {
            if (contributionModel.Message != null && contributionModel.Message != "")
            {
                if (contributionModel.Title.Length < 60)
                {
                    return true;
                }
            }
            return false;
        }

        private void ContributionNew()
        {
            if(!CheckModel(contributionModel))
            {
                return;
            }
            mediator.Send(new NewContributionMessage());
            contributionModel.Id = Guid.NewGuid();
            contributionRepository.Insert(contributionModel);
            contributionModel.Time = DateTime.Now;
            contributionModel.Group = LoggedUserID.actualGroupModel;
            contributionModel.User = LoggedUserID.userModel;
            contributionRepository.dbContext.RemoveEntities<UserEntity>();
            contributionRepository.dbContext.RemoveEntities<GroupEntity>();
            contributionRepository.Update(contributionModel);
            contributionRepository.dbContext.SaveChanges();
            LoadContributions();
            contributionModel.Title = null;
            contributionModel.Message = null;
            OnPropertyChanged("contributionModel");
        }


        public void LoadContributions()
        {
            Contributions.Clear();
            if (LoggedUserID.actualGroupModel != null)
            {
                GroupModel group = LoggedUserID.actualGroupModel;
                //Get All ContributionsByGroupId
                var contr = contributionRepository.GetAllContributionsInGroupById(group.Id);
                foreach (var item in contr)
                {
                    item.Comments = commentRepository.GetAllCommentsOfContributionById(item.Id);
                    Contributions.Add(item);
                }
            }
        }
    }
}
