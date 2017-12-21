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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICacheSubscriber, RedisCacheSubscriber>();
            services.AddScoped<ILocalCacheProvider, MemoryCacheProvider>();
            services.AddScoped<IRemoteCacheProvider, RedisCacheProvider>();
            services.AddScoped<ISerializer, JsonSerializer>();
            services.AddScoped<IDemoService, DemoService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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


            var subscriber = app.ApplicationServices.GetRequiredService<ICacheSubscriber>();                   

            //channel name should read from database or settings
            subscriber.Subscribe("CacheAdd", NotifyType.Add);
            subscriber.Subscribe("CacheUpdate", NotifyType.Update);
            subscriber.Subscribe("CacheDelete", NotifyType.Delete);
        }
    }
}
