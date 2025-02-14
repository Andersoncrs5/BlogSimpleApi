using BlogSimpleApi.Datas;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.SetRepositories.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLike(Like like)
        {
            try
            {
                if (like is null)
                    throw new ArgumentNullException(nameof(like), "Like is required");

                await this._context.Likes.AddAsync(like);

                return true;
            }
            catch(Exception e)
            {
                throw new Exception($"Error: {e}");
            }
        }

        public async Task<bool> ExistsLike(long IdUser, long IdPost)
        {
            try
            {
                if (IdUser == 0 || IdPost == 0)
                    throw new ArgumentNullException("IdPost and IdUser is required");

                Like likeFound = await this._context.Likes
                    .AsNoTracking().FirstOrDefaultAsync(l => l.UserId == IdUser && l.PostId == IdPost);

                if (likeFound is null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e}");
            }
        }

        public async Task<bool> RemoveLike(Like like)
        {
            try
            {
                if (like is null)
                    throw new ArgumentNullException(nameof(like), "Like is required");

                this._context.Likes.Remove(like);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e}");
            }
        }

        public async Task<List<Like>> GetAllUserAsync(long id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                List<Like> LikeList =
                    await _context.Likes.Where(f => f.UserId == id).ToListAsync();

                return LikeList;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task DeleteAllUserAsync(long id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                List<Like> LikeList =
                    await _context.Likes.Where(f => f.UserId == id).ToListAsync();

                this._context.Likes.RemoveRange(LikeList);
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task DeleteAllPostsAsync(long id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                List<Like> LikeList =
                    await _context.Likes.Where(f => f.PostId == id).ToListAsync();

                this._context.Likes.RemoveRange(LikeList);
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<long> GetAmount(long id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                long amount = await _context.Likes.Where(l => l.PostId == id).CountAsync();

                return amount;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);

            }
        }

        public async Task<Like> GetAsync(long IdUser, long IdPost)
        {
            try
            {
                if (IdUser == 0 || IdPost == 0)
                    throw new ArgumentNullException("IdPost and IdUser is required");

                Like likeFound = await this._context.Likes
                    .AsNoTracking().FirstOrDefaultAsync(l => l.UserId == IdUser && l.PostId == IdPost);

                if (likeFound is null)
                    return null;

                return likeFound;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e}");
            }
        }
    }
}
