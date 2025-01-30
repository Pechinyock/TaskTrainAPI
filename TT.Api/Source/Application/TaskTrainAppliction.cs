using TT.Core;

namespace TT.Api;

public class TaskTrainAppliction : ITTApp
{
    #region Startup initializer
    private class Initialize
    {
        public IConfiguration Configuration { get; }

        private readonly string _pgConnection;

        public Initialize(IHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/appsettings.json", false, true)
                .AddJsonFile($"Config/appsettings.{env.EnvironmentName}.json")
                .Build();

            _pgConnection = Configuration["Storage:PostgreSQL:Connectionstring"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwtAuth();
            services.AddControllers();
            services.AddSwaggerGenAuth();
            services.AddDbInfoService(_pgConnection);
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
        {
            builder.UseRouting();

            builder.UseAuthentication();
            builder.UseAuthorization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            builder.UseSwagger();
            builder.UseSwaggerUI();
        }
    }
    #endregion

    private IHost _app;

    public void Build(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Initialize>()
                    .UseKestrel(options => { })
                    .UseUrls("http://*:5001");
            });
        _app = builder.Build();
    }

    public void Run() => _app.Run();
    
}
