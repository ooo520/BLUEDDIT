using Microsoft.AspNetCore.Mvc;
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
            builder.Services.AddRazorPages().AddRazorPagesOptions(o =>
            {
                o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });
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
        }
    }
}
