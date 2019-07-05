﻿using Auction.Buddy.Core;
using Auction.Buddy.Web.Common.Npm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddMvc();
            services.AddAuctionBuddy();
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
            if (env.IsDevelopment())
                ConfigureDevelopment(app, loggerFactory);
            else
                ConfigureProduction(app);
        }

        private void ConfigureDevelopment(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            ConfigureBase(app)
                .UseSpa(spa =>
                {
                    spa.Options.SourcePath = "client-app";
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                    var scriptRunner = new NpmScriptRunner(loggerFactory, spa.Options);
                    scriptRunner.Execute("start");
                });
        }

        private void ConfigureProduction(IApplicationBuilder app)
        {
            ConfigureBase(app)
                .UseSpaStaticFiles();
        }

        private IApplicationBuilder ConfigureBase(IApplicationBuilder app)
        {
            return app.UseHttpsRedirection()
                .UseHsts()
                .UseAuthentication()
                .UseMvc();
        }
    }
}