﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Order.Persistence;
using Microsoft.AspNetCore.Identity;
namespace Order.WebApi
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
            
            services.AddMvc();
         
            services.AddDbContext<PizzaOrderContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PizzaOrderContext"), b => b.MigrationsAssembly("Order.WebApi")));
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<PizzaOrderContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            var dbContext = serviceProvider.GetRequiredService<PizzaOrderContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();
            app.UseMvc();
            DBInitializer.Initialize(dbContext, userManager);
        }
    }
}
