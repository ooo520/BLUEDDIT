using AutoMapper;
using Bluedit.DataAccess;
using Bluedit.DataAccess.Interfaces;

namespace Bluedit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

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
        }
    }
}
