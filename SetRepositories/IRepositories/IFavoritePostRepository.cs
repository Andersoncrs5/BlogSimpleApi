using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories
{
    public interface IFavoritePostRepository
    {
        Task<bool> CreateAsync(FavoritePost favoritePost);
        Task<bool> ExistAsync(FavoritePost favoritePost);
        Task<bool> DeleteAsync(FavoritePost favoritePost);
        Task<FavoritePost> GetAsync(long IdUser, long IdPost);
        Task<List<FavoritePost>> GetAllUserAsync(long id);
        Task DeleteAllUserAsync(long id);
        Task DeleteAllPostsAsync(long id);
        Task<long> GetAmount(long id);
    }
}
