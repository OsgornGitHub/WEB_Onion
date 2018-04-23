using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEB.Repo;
using WEB.Repo.Intarfaces;
using WEB.Repo.Repositories;
using WEB.Service.Interfaces;
using WEB.Service.Service;

namespace WEB.Web
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
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(ArtistRepository));
            services.AddScoped(typeof(IRepository<>), typeof(AlbumRepository));
            services.AddScoped(typeof(IRepository<>), typeof(SimilarRepository));
            services.AddScoped(typeof(IRepository<>), typeof(TrackRepository));
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<ITrackService, TrackService>();
            services.AddTransient<ISimilarService, SimilarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
