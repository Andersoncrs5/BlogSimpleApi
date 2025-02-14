using BlogSimpleApi.SetRepositories.IRepositories;

namespace BlogSimpleApi.SetUnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IFavoritePostRepository FavoritePostRepository { get; }
        IAdmRepository AdmRepository { get; }
        ILikeRepository LikeRepository { get; }

        Task CommitAsync();
    }
}
