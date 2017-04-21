using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GummiBearKingdom.Models;

namespace GummiBearKingdom
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddDbContext<GummiBearKingdomContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<GummiBearKingdomContext>();
            AddTestData(context);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();

            app.Run(async (context1) =>
            {
                await context1.Response.WriteAsync("Hello World!");
            });
        }

        private static void AddTestData(GummiBearKingdomContext context)
        {
            var testProduct1 = new Models.Product
            {
                ProductName = "Sour Grape",
                ProductCost = "9.99",
                ProductCountry = "USA"

            };
            context.Products.Add(testProduct1);

            var testProduct2 = new Models.Product
            {
                ProductName = "Cadbury Dairy Milk",
                ProductCost = "4.99",
                ProductCountry = "UK"

            };
            context.Products.Add(testProduct2);

            var testProduct3 = new Models.Product
            {
                ProductName = "Elite Milk Chocolate Bar with Popping Candies",
                ProductCost = "7.00",
                ProductCountry = "Israel"

            };
            context.Products.Add(testProduct3);

            var testProduct4 = new Models.Product
            {
                ProductName = "White Rabbit Creamy Candy",
                ProductCost = "12.50",
                ProductCountry = "USA"

            };
            context.Products.Add(testProduct4);

            var testProduct5 = new Models.Product
            {
                ProductName = "Chimes Ginger Chews",
                ProductCost = "3.09",
                ProductCountry = "Indonesia"

            };
            context.Products.Add(testProduct5);

            context.SaveChanges();
        }
    }
}
