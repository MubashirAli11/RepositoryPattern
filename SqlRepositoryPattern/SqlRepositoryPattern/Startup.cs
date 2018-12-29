using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.BLL;
using BusinessLogicLayer.IBLL;
using Core.DBContext;
using DALRepository.IRepositories;
using DALRepository.Repositories;
using DALRepository.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SqlRepositoryPattern
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
            configureApplicationDBContext(services);
            configureBLL(services);
            configureDALRepositories(services);
            configureUnitOfWorks(services);
            services.AddMvc();
        }

        private void configureApplicationDBContext(IServiceCollection services)
        {
            services.AddDbContext<LifebookDbContext>(
            options => options.UseSqlServer(Configuration
            .GetConnectionString("LifebookConnectionString")),
            ServiceLifetime.Scoped);
        }

        private void configureBLL(IServiceCollection services)
        {
            services.AddScoped<IMovieBLL, MovieBLL>();
        }

        private void configureDALRepositories(IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
        }

        private void configureUnitOfWorks(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LifebookDbContext dbContext)
        {
            //do not open
            //dbContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
