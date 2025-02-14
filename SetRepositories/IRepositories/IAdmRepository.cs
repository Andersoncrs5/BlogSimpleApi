using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories
{
    public interface IAdmRepository
    {
        Task UnBlockOrBlockPost(Post post);
        Task UnBlockOrBlockComment(Comment comment);
        Task UnBlockOrBlockUser(User user);
        Task<bool> IsBlockedUser(User user);
        Task<List<User>> ListOfBlockUser();
        Task<List<Comment>> ListOfBlockComments();
        Task<List<Post>> ListOfBlockPosts();
    }
}
