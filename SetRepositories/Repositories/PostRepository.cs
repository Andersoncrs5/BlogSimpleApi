using BlogSimpleApi.Datas;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.SetRepositories.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        try
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post), "Post is required");

            var postCreated = await this._context.Posts.AddAsync(post);

            return postCreated.Entity;
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task DeleteAsync(Post post)
    {

        try
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post), "Post is required");

            this._context.Posts.Remove(post);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public Task<List<Post>> GetAllAsync()
    {
        try
        {
            return this._context.Posts.AsNoTracking()
                .Where(p => p.IsBlock == false && p.IsBlockByUser == false).ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task<List<Post>> GetAllUserAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "id is required");

            return await this._context.Posts.AsNoTracking().Where(p => p.UserId == id).ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }
    
    public async Task<Post> GetAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "id is required");

            return await this._context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        try
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post), "Post is required");

            post.IsEdited = true;

            _context.Entry(post).State = EntityState.Modified;
            return post;
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task DeleteAllAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "id is required");

            var posts = await this._context.Posts.AsNoTracking().Where(p => p.UserId == id).ToListAsync();

            _context.Posts.RemoveRange(posts);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

}
