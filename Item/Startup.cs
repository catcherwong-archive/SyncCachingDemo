namespace Item
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Lib;
    using Item.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private ICacheSubscriber _cacheSubscriber;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICacheSubscriber, RedisCacheSubscriber>();
            services.AddScoped<ILocalCacheProvider, MemoryCacheProvider>();
            services.AddScoped<IRemoteCacheProvider, RedisCacheProvider>();

            services.AddScoped<IDemoService,DemoService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICacheSubscriber cacheSubscriber)
        {
            if (env.IsDevelopment())
            {
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

            _cacheSubscriber = cacheSubscriber;

            //channel name should read from database or settings
            _cacheSubscriber.Subscribe("CacheAdd", NotifyType.Add);
            _cacheSubscriber.Subscribe("CacheUpdate", NotifyType.Update);
            _cacheSubscriber.Subscribe("CacheDelete", NotifyType.Delete);
        }
    }
}
