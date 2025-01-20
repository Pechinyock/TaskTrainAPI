using Microsoft.OpenApi.Models;
using TT.ExtensionMethods;
using TT.Core.Interfaces;
using TT.Models.Authentication;
using TT.Services.Interafces;
using TT.Services;

namespace TT.Application;

public class TaskTrainAppliction : IApplication
{
    #region Startup initializer
    private class Initialize
    {
        public IConfiguration Configuration { get; }

        private readonly AuthOptionsModel _authOptions;
        private readonly string _pgConnection;

        public Initialize(IHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/appsettings.json", false, true)
                .AddJsonFile($"Config/appsettings.{env.EnvironmentName}.json")
                .Build();

            _authOptions = new AuthOptionsModel()
            {
                Issuer = Configuration["Jwt:Issuer"],
                Audience = Configuration["Jwt:Audience"],
                Key = Configuration["Jwt:Key"],
                Lifetime = UInt32.Parse(Configuration["Jwt:Lifetime"])
            };

            /* [TODO] Validate */
            _pgConnection = Configuration["Storage:PostgreSQL:Connectionstring"];

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwt(_authOptions);
            services.AddControllers();
            services.AddSwaggerGen();

            /* [TODO] to extensiont method */
            services.AddTransient<IDatabaseInfoService, DatabaseInfoService>();
            services.Configure<DatabaseInfoOptions>(options => 
            {
                options.ConnectionString = _pgConnection;
            });
            services.AddSwaggerGen(options => 
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    new List<string>()
                }
                });
            });
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
