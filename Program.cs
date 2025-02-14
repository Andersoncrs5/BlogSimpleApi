using BlogSimpleApi.Datas;
using BlogSimpleApi.SetRepositories.IRepositories;
using BlogSimpleApi.SetRepositories.Repositories;
using BlogSimpleApi.SetUnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Configura��o do Kestrel para definir portas
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Porta HTTP
    options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // Porta HTTPS
});

// Adicionando servi�os ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Blog",
        Version = "v1",
        Description = "Documenta��o da API usando Swagger",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Anderson",
            Email = "anderson.c.rms2005@gmail.com",
            Url = new Uri("https://seusite.com")
        }
    });
});

// Inje��o de depend�ncias dos reposit�rios e unidade de trabalho
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFavoritePostRepository, FavoritePostRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IAdmRepository, AdmRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Middleware para Swagger (somente em ambiente de desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicando pol�tica de CORS antes dos controllers
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
