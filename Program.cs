using Dis.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();

           
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Kimlik doğrulama ve cookie ayarları
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login"; // Giriş yapılmadığında yönlendirilecek sayfa
                    options.AccessDeniedPath = "/Auth/Login"; // Yetkisiz erişim durumunda yönlendirilecek sayfa
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                });

            // Yetkilendirme servisi
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Ortam 
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // Kimlik doğrulama ve yetkilendirmeyi sırayla kullan
            app.UseAuthentication();
            app.UseAuthorization();

            // Varsayılan rota
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
