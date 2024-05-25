using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using bluedit.DataAccess;
using bluedit.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bluedit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddEntityFrameworkSqlServer()
                        .AddDbContext<bluedit.DataAccess.EfModels.BlueditContext>(options => options.UseSqlServer("name=ConnectionStrings:bluedit"));

            builder.Services.AddAutoMapper(typeof(bluedit.DataAccess.AutomapperProfiles));
            builder.Services.AddRazorPages().AddRazorPagesOptions(o =>
            {
                o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });

            builder.Services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddTransient<bluedit.DataAccess.Interfaces.IAnswerRepository, bluedit.DataAccess.AnswerRepository>();
            builder.Services.AddTransient<bluedit.DataAccess.Interfaces.ICategoryRepository, bluedit.DataAccess.CategoryRepository>();
            builder.Services.AddTransient<bluedit.DataAccess.Interfaces.IOpinionRepository, bluedit.DataAccess.OpinionRepository>();
            builder.Services.AddTransient<bluedit.DataAccess.Interfaces.IThreadRepository, bluedit.DataAccess.ThreadRepository>();
            builder.Services.AddTransient<bluedit.DataAccess.Interfaces.IUserRepository, bluedit.DataAccess.UserRepository>();


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

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
