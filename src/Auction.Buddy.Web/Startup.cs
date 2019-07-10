using Auction.Buddy.Core;
using Auction.Buddy.Web.Common.Npm;
using Auction.Buddy.Web.Common.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;

namespace Auction.Buddy.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(opts => opts.RootPath = "wwwroot");
            services.AddNodeServices();
            services.AddMvc()
                .AddMvcOptions(opts =>
                {
                    opts.Filters.Add<ValidationExceptionFilter>();
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    opts.Filters.Add(new AuthorizeFilter(policy));
                });

            services.AddAuctionBuddy(
                dbOptions => dbOptions.UseInMemoryDatabase("AuctionsDb")
            );

            services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opts =>
                {
                    opts.Authority = Configuration.IdentityAuthority();
                    opts.Audience = Configuration.IdentityAudience();
                    opts.RequireHttpsMetadata = true;
                    opts.RefreshOnIssuerKeyNotFound = true;
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            IdentityModelEventSource.ShowPII = !env.IsProduction();

            if (env.IsDevelopment()) 
                app.UseDeveloperExceptionPage();

            app.UseHsts()
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseSpaStaticFiles();

            app.UseMvc()
                .UseSpa(spa =>
                {
                    spa.Options.SourcePath = env.IsDevelopment() ? "client-app" : "wwwroot";
                    if (!env.IsDevelopment()) 
                        return;
                    
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                    var scriptRunner = new NpmScriptRunner(loggerFactory, spa.Options);
                    scriptRunner.Execute("start");
                });
        }
    }
}