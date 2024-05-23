using AutoMapper;
using Bluedit.DataAccess;
using Bluedit.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bluedit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddEntityFrameworkSqlServer()
                        .AddDbContext<Bluedit.DataAccess.EfModels.BlueditContext>(options => options.UseSqlServer("name=ConnectionStrings:Bluedit"));
            builder.Services.AddAutoMapper(typeof(Bluedit.DataAccess.AutomapperProfiles));
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddTransient<Bluedit.DataAccess.Interfaces.IAnswerRepository, Bluedit.DataAccess.AnswerRepository>();
            builder.Services.AddTransient<Bluedit.DataAccess.Interfaces.ICategoryRepository, Bluedit.DataAccess.CategoryRepository>();
            builder.Services.AddTransient<Bluedit.DataAccess.Interfaces.IOpinionRepository, Bluedit.DataAccess.OpinionRepository>();
            builder.Services.AddTransient<Bluedit.DataAccess.Interfaces.IThreadRepository, Bluedit.DataAccess.ThreadRepository>();
            builder.Services.AddTransient<Bluedit.DataAccess.Interfaces.IUserRepository, Bluedit.DataAccess.UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();

            // --------------------------------------------------------------------------------------------------
            /*
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = loggerFactory.CreateLogger("Program");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccess.AutomapperProfiles>();
            });

            var mapper = config.CreateMapper();
            var repo = new DataAccess.CategoryRepository(new DataAccess.EfModels.BlueditContext(), logger, mapper);

            repo.Create(new Dbo.Shortcut() { Url = "http://localhost:8080", Hash = "000000000", SessionId = 1 }).Wait();
            var T = repo.Read();
            T.Wait();
            Dbo.Shortcut shortcut = T.Result.First();
            Console.WriteLine(shortcut.Url + shortcut.Id);
            shortcut.Url = "http://localhost:8000";
            shortcut = repo.Update(shortcut).Result;
            Console.WriteLine(shortcut.Url + shortcut.Id);

            repo.Delete(2).Wait();
            */

            ILogger<CategoryRepository> logger = app.Services.GetRequiredService<ILogger<CategoryRepository>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccess.AutomapperProfiles>();
            });

            var mapper = config.CreateMapper();
            var repo = new DataAccess.CategoryRepository(new DataAccess.EfModels.BlueditContext(), logger, mapper);

            //repo.Create(new Dbo.Category() { Name = "dhxlcdvkmlcvfjkclsmvfkjl", Title = "kjdfhsldckksdkfsl" }).Wait();
            //var T = repo.Read();
            // Dbo.Category shortcut = T.Result.First();
            //T.Wait();
            //Console.WriteLine(shortcut.Title + shortcut.Id);
            //shortcut.Title = "Un titre";
            //shortcut = repo.Update(shortcut).Result;
            //Console.WriteLine(shortcut.Title + shortcut.Id);

            //repo.Delete(shortcut.Id).Wait();
            app.Run();
        }
    }
}
