using BlogSimpleApi.Datas;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.SetRepositories.Repositories
{
    public class AdmRepository : IAdmRepository
    {
        private readonly AppDbContext _context;

        public AdmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsBlockedUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("User is required");

                if (user.IsBlock == false)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error:\n{e}");
            }
        }

        public async Task<List<Comment>> ListOfBlockComments()
        {
            try
            {
                List<Comment> list = await _context.Comments.Where(c => c.IsBlock == true).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception($"Error:\n{e}");
            }
        }

        public async Task<List<Post>> ListOfBlockPosts()
        {
            try
            {
                List<Post> list = await _context.Posts.Where(p => p.IsBlock == true).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception($"Error:\n{e}");
            }
        }

        public async Task<List<User>> ListOfBlockUser()
        {
            try
            {
                List<User> list = await _context.Users.Where(u => u.IsBlock == true).ToListAsync();
                return list;
            }
            catch(Exception e)
            {
                throw new Exception($"Error:\n{e}");
            }

        }

        public async Task UnBlockOrBlockComment(Comment comment)
        {
            try
            {
                if (comment == null)
                    throw new ArgumentNullException("Comment is required");

                comment.IsBlock = !comment.IsBlock;
                _context.Entry(comment).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task UnBlockOrBlockPost(Post post)
        {
            try
            {
                if (post == null)
                    throw new ArgumentNullException("Comment is required");


                List<Comment> comments = await this._context
                    .Comments.Where(c => c.PostId == post.Id).ToListAsync();

                foreach (Comment comment in comments) 
                {
                    comment.IsBlockByPost = !comment.IsBlockByPost;
                }

                post.IsBlock = !post.IsBlock;

                foreach (Comment comment in comments)
                {
                    _context.Entry(comment).State = EntityState.Modified;
                }

                _context.Entry(post).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task UnBlockOrBlockUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("Comment is required");


                List<Comment> comments = await this._context
                    .Comments.Where(c => c.UserId == user.Id).ToListAsync();

                foreach (Comment comment in comments)
                {
                    comment.IsBlockByPost = !comment.IsBlockByPost;
                }

                List<Post> posts = await this._context
                    .Posts.Where(p => p.UserId == user.Id).ToListAsync();

                foreach (Post post in posts)
                {
                    post.IsBlockByUser = !post.IsBlockByUser;
                }

                foreach (Comment comment in comments)
                {
                    _context.Entry(comment).State = EntityState.Modified;
                }

                foreach (Post post in posts)
                {
                    _context.Entry(post).State = EntityState.Modified;
                }

                user.IsBlock = !user.IsBlock;

                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }
    }
}
