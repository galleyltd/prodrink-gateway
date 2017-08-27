using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using prodrink.gateway.Data;
using prodrink.gateway.Models;
using prodrink.gateway.Services;
using prodrink.gateway.Services.Grpc;

namespace prodrink.gateway
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
            services.AddLogging();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            var secretsStorageService = new SecretsStorageService();
            services.AddTransient<ISecretsStorageService>(provider => secretsStorageService);
            AddGrpcProviders(services);

            var postgresHost = secretsStorageService.GetPostgresHost();
            var connectionString = $"host={postgresHost};database=postgres;user id=postgres; Password=postgres";
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = secretsStorageService.GetGoogleAuthClientId();
                googleOptions.ClientSecret = secretsStorageService.GetGoogleAuthClientSecret();
            });

            // Add framework services.
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "ProDrink API",
                    Version = "v1",
                    Description = "Drink like a PRO",
                    TermsOfService = "Terms Of Service"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        private static void AddGrpcProviders(IServiceCollection services)
        {
            services.AddSingleton<CatalogProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
        }
    }
}