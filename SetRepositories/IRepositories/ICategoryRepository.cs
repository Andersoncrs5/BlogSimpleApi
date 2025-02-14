using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
        Task DeleteAllUserAsync(long id);
        Task<Category> GetAsync(long id);
        Task<List<Category>> GetAllAsync();
    }
}