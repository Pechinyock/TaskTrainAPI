using TT.ExtensionMethods;
using TT.Interfaces;
using TT.Models.Authentication;
using TT.Security;

namespace TT;

public class TaskTrainAppliction : IApplication
{
    #region Startup initializer
    private class Initialize
    {
        public IConfiguration Configuration { get; }

        public Initialize(IHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/appsettings.json", false, true)
                .AddJsonFile($"Config/appsettings.{env.EnvironmentName}.json")
                .Build();

            var authOptions = new AuthOptionsModel()
            {
                Issuer = Configuration["Jwt:Issuer"],
                Audience = Configuration["Jwt:Audience"],
                Key = Configuration["Jwt:Key"],
                Lifetime = UInt32.Parse(Configuration["Jwt:Lifetime"])
            };
            Authentication.Options = authOptions;
            Console.WriteLine($"Configure with: {env.EnvironmentName}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwt(Authentication.Options);
            services.AddControllers();
            services.AddSwaggerGen();
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
                    .UseUrls("http://*:5000");
            });
        _app = builder.Build();
    }

    public void Run()
    {
        _app.Run();
    }
}
