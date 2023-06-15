using Divena_CMS.Data;
using Divena_CMS.Interface;
using Divena_CMS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Divena_CMS
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddMemoryCache(); // Add this line
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/User/AccessDenied";
            });
            services.AddDbContext<Divena_CMSContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Divena_CMSContext")));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<Divena_CMSContext>().AddDefaultTokenProviders();

            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));

            //Read Email Setting
            services.AddTransient<IEmailHelper, EmailHelper>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error/404");
            }
            else
            {
                app.UseExceptionHandler("/Error/404");
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                //routes.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Menu}/{action=Index}/{id?}");

                /*Menu*/
                routes.MapControllerRoute(
                name: "PagingPageOne-menu",
                pattern: "Menu",
                defaults: new { controller = "Menu", action = "Index", id = 1 }
                );

                routes.MapControllerRoute(
                name: "Paging-menu",
                pattern: "Menu/{id:int?}",
                defaults: new { controller = "Menu", action = "Index" });
                /*End*/

                /*Page*/
                routes.MapControllerRoute(
                name: "PagingPageOne-page",
                pattern: "Page",
                defaults: new { controller = "Page", action = "Index", id = 1 }
                );

                routes.MapControllerRoute(
                name: "Paging-page",
                pattern: "Page/{id:int?}",
                defaults: new { controller = "Page", action = "Index" });
                /*End*/

                /*MyBlogCategory*/
                routes.MapControllerRoute(
                name: "PagingPageOne-myblogcategory",
                pattern: "MyBlogCategory/{url}",
                defaults: new { controller = "Home", action = "MyBlogCategory", id = 1 }
                );

                routes.MapControllerRoute(
                name: "Paging-myblogcategory",
                pattern: "MyBlogCategory/{url}/{id:int?}",
                defaults: new { controller = "Home", action = "MyBlogCategory" });
                /*End*/

                /*MyBlog*/
                routes.MapControllerRoute(
                name: "PagingPageOne-myblog",
                pattern: "MyBlog",
                defaults: new { controller = "Home", action = "MyBlog", id = 1 }
                );

                routes.MapControllerRoute(
                name: "Paging-myblog",
                pattern: "MyBlog/{id:int?}",
                defaults: new { controller = "Home", action = "MyBlog" });
                /*End*/

                /*Blog*/
                routes.MapControllerRoute(
                name: "PagingPageOne-blog",
                pattern: "Blog",
                defaults: new { controller = "Blog", action = "Index", id = 1 }
                );

                routes.MapControllerRoute(
                name: "Paging-blog",
                pattern: "Blog/{id:int?}",
                defaults: new { controller = "Blog", action = "Index" });
                /*End*/

                /*Blog Category*/
                routes.MapControllerRoute(
                name: "PagingPageOne-blogcategory",
                pattern: "BlogCategory",
                defaults: new { controller = "BlogCategory", action = "Index", id = 1 }
                );

                routes.MapControllerRoute(
                name: "Paging-blogcategory",
                pattern: "BlogCategory/{id:int?}",
                defaults: new { controller = "BlogCategory", action = "Index" });
                /*End*/

                /*View Blog*/
                routes.MapControllerRoute(
                name: "View-Blog",
                pattern: "{url}-id-{id}",
                defaults: new { controller = "Home", action = "ViewBlog" });
                /*End*/

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                /*View Page*/
                routes.MapControllerRoute(
                name: "View-Page",
                pattern: "{url}",
                defaults: new { controller = "Home", action = "Page" });
                /*End*/


            });
        }
    }
}
