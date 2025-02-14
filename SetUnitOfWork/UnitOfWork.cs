using BlogSimpleApi.SetRepositories.IRepositories;
using BlogSimpleApi.SetRepositories.Repositories;
using BlogSimpleApi.Datas;

namespace BlogSimpleApi.SetUnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private IUserRepository _userRepo;
        private IPostRepository _postRepo;
        private ICommentRepository _commentRepo;
        private ICategoryRepository _categoryRepo;
        private IFavoritePostRepository _favoritePostRepo;
        private ILikeRepository _likeRepo;
        private IAdmRepository _admRepo;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository =>
            _userRepo ??= new UserRepository(_context);

        public IPostRepository PostRepository =>
            _postRepo ??= new PostRepository(_context);

        public ICommentRepository CommentRepository =>
            _commentRepo ??= new CommentRepository(_context);

        public ICategoryRepository CategoryRepository =>
            _categoryRepo ??= new CategoryRepository(_context);

        public IFavoritePostRepository FavoritePostRepository =>
            _favoritePostRepo ??= new FavoritePostRepository(_context);

        public ILikeRepository LikeRepository => _likeRepo ??= new LikeRepository(_context);

        public IAdmRepository AdmRepository => _admRepo ??= new AdmRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
