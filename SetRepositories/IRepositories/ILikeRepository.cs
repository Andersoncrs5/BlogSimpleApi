using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories
{
    public interface ILikeRepository
    {
        Task<bool> ExistsLike(long IdUser, long IdPost);
        Task<bool> CreateLike(Like like);
        Task<bool> RemoveLike(Like like);
        Task<List<Like>> GetAllUserAsync(long id);
        Task<Like> GetAsync(long IdUser, long IdPost);
        Task DeleteAllUserAsync(long id);
        Task DeleteAllPostsAsync(long id);
        Task<long> GetAmount(long id);
    }
}