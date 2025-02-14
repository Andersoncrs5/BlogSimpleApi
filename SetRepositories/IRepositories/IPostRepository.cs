using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories;

public interface IPostRepository
{
    Task<Post> CreateAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task DeleteAsync(Post post);
    Task DeleteAllAsync(long id);
    Task<Post> GetAsync(long id);
    Task<List<Post>> GetAllAsync();
    Task<List<Post>> GetAllUserAsync(long id);
}