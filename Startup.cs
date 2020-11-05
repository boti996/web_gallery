using System.Reflection;
using System.Collections.Immutable;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web_gallery.Models;
using Microsoft.Extensions.Options;
using web_gallery.Services;
using AspNetCore.Identity.Mongo;

namespace web_gallery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        protected void ConfigureDatabase<T, I>(IServiceCollection services)
            where T : class, I, new()
            where I : class, IDatabaseSettings
        {
            services.Configure<T>(
                Configuration.GetSection(typeof(T).Name));

            services.AddSingleton<I>(sp =>
                sp.GetRequiredService<IOptions<T>>().Value);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureDatabase<UsersDatabaseSettings, IUsersDatabaseSettings>(services);
            this.ConfigureDatabase<MediaDatabaseSettings, IMediaDatabaseSettings>(services);

            services.AddSingleton<Services.AlbumService>();
            services.AddSingleton<Services.VideoService>();
            services.AddSingleton<Services.UserService>();
            services.AddSingleton<Services.TokenService>();

            services.AddIdentityMongoDbProvider<
                AspNetCore.Identity.Mongo.Model.MongoUser, 
                AspNetCore.Identity.Mongo.Model.MongoRole>(identityOptions =>
                {
                    // Password settings
                    identityOptions.Password.RequiredLength = 6;
                    identityOptions.Password.RequireLowercase = false;
                    identityOptions.Password.RequireUppercase = false;
                    identityOptions.Password.RequireNonAlphanumeric = false;
                    identityOptions.Password.RequireDigit = false;
                    // Lockout settings
                    identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                    identityOptions.Lockout.AllowedForNewUsers = true;
                    // User settings
                    identityOptions.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    identityOptions.User.RequireUniqueEmail = false;
                
                }, mongoIdentityOptions => {
                    mongoIdentityOptions.ConnectionString = "mongodb://localhost:27017/UserDb";
                });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(PolicyNames.RequireAdminRole, policy => 
                {
                    policy.RequireRole("Admin");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
