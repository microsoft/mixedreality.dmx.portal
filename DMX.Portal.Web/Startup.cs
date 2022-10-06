// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Brokers.DateTimes;
using DMX.Portal.Web.Brokers.DmxApis;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Services.Foundations.LabCommands;
using DMX.Portal.Web.Services.Foundations.Labs;
using DMX.Portal.Web.Services.Views.LabViews;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Syncfusion.Blazor;

namespace DMX.Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddServerSideBlazor();
                //.AddMicrosoftIdentityConsentHandler();

            services.AddSyncfusionBlazor();
            services.AddHttpClient();
            //services.AddSyncfusionBlazor();
            AddBrokers(services);
            AddServices(services);
            //AddSecurity(services);

            services.AddRazorPages(options =>
                options.RootDirectory = "/Views/Pages"
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            MapControllersForEnvironments(app, env);
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IDmxApiBroker, DmxApiBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<ILabService, LabService>();
            services.AddTransient<ILabCommandService, LabCommandService>();
            services.AddTransient<ILabViewService, LabViewService>();
        }

        private void AddSecurity(IServiceCollection services)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
                    .EnableTokenAcquisitionToCallDownstreamApi()
                        .AddDownstreamWebApi(
                            "DownstreamApi", Configuration.GetSection("DownstreamApi"))
                                .AddInMemoryTokenCaches();

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddAuthorization(options =>
                options.FallbackPolicy = options.DefaultPolicy);
        }

        private static void MapControllersForEnvironments(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment())
                {
                    endpoints.MapBlazorHub().AllowAnonymous();
                    endpoints.MapFallbackToPage("/_Host").AllowAnonymous();
                }
                else
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                }
            });
        }
    }
}
