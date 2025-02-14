using BlogSimpleApi.Datas;
using BlogSimpleApi.DTOs;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.SetRepositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User is required");

                user.Password = this.Hash(user.Password);

                var result = await _context.Users.AddAsync(user);

                return result.Entity; 
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task DeleteAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User is required");

                _context.Users.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<User> GetAsync(long id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<bool> LoginAsync(LoginUserDTO dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "User is required");

                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (user == null)
                    return false;

                bool check = await this.CheckPassword(dto.Password, user.Password);
                return check;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User is required");

                _context.Entry(user).State = EntityState.Modified;

                return user;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        private string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); 
        }

        private async Task<bool> CheckPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash); 
        }

        public async Task<bool> ValidEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }

        public async Task<User> ChangeStatusAdm(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User is required");

                user.IsAdm = !user.IsAdm;

                _context.Entry(user).State = EntityState.Modified;

                return user;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }
    }
}
