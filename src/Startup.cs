using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MtnMomo.NET;

namespace momotest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages();
            services.AddSingleton(_ =>
            {
                var config = new MomoConfig
                {
                    UserId = Configuration["Momo:Collections:UserId"],
                    UserSecret = Configuration["Momo:Collections:UserSecret"],
                    SubscriptionKey = Configuration["Momo:Collections:SubscriptionKey"]
                };
                return new CollectionsClient(config);
            });
            services.AddSingleton(_ =>
            {
                var config = new MomoConfig
                {
                    UserId = Configuration["Momo:Disbursements:UserId"],
                    UserSecret = Configuration["Momo:Disbursements:UserSecret"],
                    SubscriptionKey = Configuration["Momo:Disbursements:SubscriptionKey"]
                };
                return new DisbursementsClient(config);
            });
            services.AddSingleton(_ =>
            {
                var config = new MomoConfig
                {
                    UserId = Configuration["Momo:Remittances:UserId"],
                    UserSecret = Configuration["Momo:Remittances:UserSecret"],
                    SubscriptionKey = Configuration["Momo:Remittances:SubscriptionKey"]
                };
                return new RemittancesClient(config);
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
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
