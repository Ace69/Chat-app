using Communication.BL.Repositories;
using Communication.BL.Services;
using Communication.DAL;

namespace Communication.App.ViewModels
{
    public class ViewModelLocator : ViewModelBase
    {
        private readonly GroupRepository groupRepository;
        private readonly ContributionRepository contributionRepository;
        private readonly CommentRepository commentRepository;
        private readonly UserRepository userRepository;
        private readonly GroupMemberRepository groupMemberRepository;


        private readonly IMediator mediator;

        public ViewModelLocator()
        {
            mediator = new Mediator();
            groupRepository = new GroupRepository(new CommunicationDbContext());
            userRepository = new UserRepository(new CommunicationDbContext());
            contributionRepository = new ContributionRepository(new CommunicationDbContext());
            commentRepository = new CommentRepository(new CommunicationDbContext());
            groupMemberRepository = new GroupMemberRepository(new CommunicationDbContext());
        }

        public GroupListViewModel GroupListViewModel => new GroupListViewModel(groupRepository, mediator);
        public NewGroupViewModel NewGroupViewModel => new NewGroupViewModel(groupRepository, groupMemberRepository ,mediator);
        public LoggedUserViewModel LoggedUserViewModel => new LoggedUserViewModel(userRepository, groupRepository, mediator);
        public ContributionViewModel ContributionViewModel => new ContributionViewModel(contributionRepository, commentRepository, mediator);
        public CommentViewModel CommentViewModel => new CommentViewModel(commentRepository, mediator);
        public MainMenuViewModel MainMenuViewModel => new MainMenuViewModel(mediator);
        public LoggedUserProfileDetailViewModel LoggedUserProfileDetailViewModel => new LoggedUserProfileDetailViewModel(userRepository, mediator);
        public SeachUserViewModel SeachUserViewModel => new SeachUserViewModel(userRepository, mediator);
        public AdduserViewModel AdduserViewModel => new AdduserViewModel(groupMemberRepository , userRepository ,mediator); /// THIS TODO     
        public AddOrDeleteGroupMemeberViewModel AddOrDeleteGroupMemeberViewModel => new AddOrDeleteGroupMemeberViewModel(userRepository, groupRepository, groupMemberRepository, mediator);
        public UsersInGroupViewModel UsersInGroupViewModel => new UsersInGroupViewModel(userRepository, mediator);
    }
}
