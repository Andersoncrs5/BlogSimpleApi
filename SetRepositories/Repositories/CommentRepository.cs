using BlogSimpleApi.Datas;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.SetRepositories.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        try
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "Comment is required");

            var result = await _context.Comments.AddAsync(comment);
            return result.Entity;
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task DeleteAllPostAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id is required");

            List<Comment> comments = await this._context
                .Comments.AsNoTracking().Where(c => c.PostId == id).ToListAsync();
            this._context.Comments.RemoveRange(comments);

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

            List<Comment> comments = await this._context
                .Comments.AsNoTracking().Where(c => c.UserId == id).ToListAsync();
            this._context.Comments.RemoveRange(comments);

        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task DeleteAsync(Comment comment)
    {
        try
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "Comment is required");

            _context.Comments.Remove(comment);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task<List<Comment>> GetAllPostAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id is required");

            return await _context.Comments
                .AsNoTracking()
                .Where(c => c.PostId == id 
                && c.IsBlock == false 
                && c.IsBlockByPost == false 
                && c.IsBlockByUser == false)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task<List<Comment>> GetAllUserAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id is required");

            return await _context.Comments
                .AsNoTracking()
                .Where(c => c.UserId == id)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task<Comment?> GetAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id is required");

            return await _context.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        try
        {
            if (comment is null)
                throw new ArgumentNullException(nameof(comment), "Comment is required");

            _context.Entry(comment).State = EntityState.Modified;
            return comment;
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}", e);
        }
    }
}
