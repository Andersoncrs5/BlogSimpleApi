using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment> UpdateAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task DeleteAllPostAsync(long id);
    Task DeleteAllUserAsync(long id);
    Task<Comment> GetAsync(long id);
    Task<List<Comment>> GetAllPostAsync(long id);
    Task<List<Comment>> GetAllUserAsync(long id);
}
