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
    public class CommentViewModel : ViewModelBase
    {
        private readonly CommentRepository commentRepository;
        private readonly IMediator mediator;

        public ObservableCollection<CommentModel> Comments { get; set; } = new ObservableCollection<CommentModel>();

        public ICommand CommentNewCommand { get; set; }
        public ICommand Back { get; set; }

        public CommentModel _commentModel;
        public CommentModel commentModel
        {
            get
            {
                return _commentModel;
            }
            set
            {
                if (_commentModel != value)
                {
                    _commentModel = value;
                    OnPropertyChanged("commentModel");
                }
            }

        }

        public CommentViewModel(CommentRepository commentRepository, IMediator mediator)
        {
            this.commentRepository = commentRepository;
            this.mediator = mediator;

            CommentNewCommand = new RelayCommand(CommentNew);
            Back = new RelayCommand(GoBack);

            mediator.Register<CloseCommentsWindowMessage>(CloseWindow);
            mediator.Register<ContributionSelected>(ShowCommentsWindow);
            mediator.Register<NewCommentMessage>(NewComment);
        }
        private void GoBack()
        {
            mediator.Send(new SelectedGroupMessage());
            commentModel = null;
        }

        private void ShowCommentsWindow(ContributionSelected obj)
        {
            ContributionModel model = LoggedUserID.actualContributionModel;
            if (obj.Id == model.Id)
            {
                commentModel = new CommentModel();
                LoadComments();
            }
        }

        private void CloseWindow(CloseCommentsWindowMessage obj)
        {
            commentModel = null;
        }

        private void NewComment(NewCommentMessage comment) => LoadComments();

        private bool CheckModel(CommentModel commentModel)
        {
            if (commentModel.Message != null && commentModel.Message != "")
            {
                if(commentModel.Message.Length < 120)
                {
                    return true;
                }
            }
            return false;
        }
        private void CommentNew()
        {
            if (!CheckModel(commentModel))
            {
                return;
            }
            mediator.Send(new NewCommentMessage());
            
            commentModel.Id = Guid.NewGuid();
            commentRepository.InsertNoSave(commentModel);

            var comment = commentRepository.dbContext.Comments.SingleOrDefault(i => i.Id == commentModel.Id);

            comment.Time = DateTime.Now;
            comment.Contribution = commentRepository.dbContext.Contributions.SingleOrDefault(i => i.Id == LoggedUserID.actualContributionModel.Id);
            comment.User = commentRepository.dbContext.Users.SingleOrDefault(i => i.Id == LoggedUserID.userModel.Id);

            commentRepository.dbContext.SaveChanges();
            LoadComments();
            commentModel.Message = null;
            OnPropertyChanged("commentModel");
        }

        public void LoadComments()
        {
            Comments.Clear();
            if (LoggedUserID.actualContributionModel != null)
            {
                ContributionModel actualContr = LoggedUserID.actualContributionModel;
                var com = commentRepository.GetAllCommentsOfContributionById(actualContr.Id);
                foreach (var item in com)
                {
                    Comments.Add(item);
                }
            }
        }
    }
}
