using BlogSimpleApi.DTOs;
using BlogSimpleApi.Models;

namespace BlogSimpleApi.SetRepositories.IRepositories;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User> GetAsync(long id);
    Task<bool> LoginAsync(LoginUserDTO dto);
    Task<bool> ValidEmailAsync(string email);
    Task<User> ChangeStatusAdm(User user);
}
