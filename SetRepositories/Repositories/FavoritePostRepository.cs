using BlogSimpleApi.Datas;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.SetRepositories.Repositories
{
    public class FavoritePostRepository : IFavoritePostRepository
    {
        private readonly AppDbContext _context;

        public FavoritePostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(FavoritePost favoritePost)
        {
            try
            {
                if (favoritePost == null)
                    throw new ArgumentNullException(nameof(favoritePost), "favoritePost is required");

                await _context.FavoritePosts.AddAsync(favoritePost);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<bool> DeleteAsync(FavoritePost favoritePost)
        {
            try
            {
                if (favoritePost == null)
                    throw new ArgumentNullException(nameof(favoritePost), "favoritePost is required");

                _context.FavoritePosts.Remove(favoritePost);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<bool> ExistAsync(FavoritePost favoritePost)
        {
            try
            {
                if (favoritePost is null)
                    throw new ArgumentNullException(nameof(favoritePost), "favoritePost is required");

                FavoritePost fp = await _context.FavoritePosts
                    .FirstOrDefaultAsync(f => f.UserId == favoritePost.UserId && f.PostId == favoritePost.PostId);

                if (fp is null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<List<FavoritePost>> GetAllUserAsync(long id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                List<FavoritePost> listFavoritePost =
                    await _context.FavoritePosts.Where(f => f.UserId == id).ToListAsync();

                return listFavoritePost;
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

                List<FavoritePost> FavoritePostList =
                    await _context.FavoritePosts.Where(f => f.UserId == id).ToListAsync();

                this._context.FavoritePosts.RemoveRange(FavoritePostList);
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

                List<FavoritePost> FavoritePostList =
                    await _context.FavoritePosts.Where(f => f.PostId == id).ToListAsync();

                this._context.FavoritePosts.RemoveRange(FavoritePostList);
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

                long amount = await _context.FavoritePosts.Where(p => p.PostId == id).CountAsync();

                return amount;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);

            }
        }

        public async Task<FavoritePost> GetAsync(long IdUser, long IdPost)
        {
            try
            {
                if (IdUser == 0 || IdPost == 0)
                    throw new ArgumentNullException("IdPost and IdUser is required");

                FavoritePost fp = await _context.FavoritePosts
                    .FirstOrDefaultAsync(f => f.UserId == IdUser && f.PostId == IdPost);

                return fp;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }
    }
}
