

using SchoolApp_MVC.ApiClients;
using SchoolApp_MVC.ApiClients.Interfaces;

namespace SchoolApp_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var banckendBaseUrl = builder.Configuration.GetValue<string>("BackendApi:BaseUrl") ?? "https://localhost:5206/";

            builder.Services.AddHttpClient<IStudentApiClient, StudentApiClient>(client =>
            {
                client.BaseAddress = new Uri(banckendBaseUrl);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
