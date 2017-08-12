﻿using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using IdentityServer4.Postgresql.Extensions;
using IdentityServer4.Postgresql.Mappers;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace prodrink.gateway
{
    internal class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddSingleton<IClientStore, CustomClientStore>();
            services.AddIdentityServer()
                .AddConfigurationStore(Configuration.GetConnectionString("PostgreSql"))
                .AddOperationalStore()
                .AddTemporarySigningCredential();
        }

        private void InitData(IApplicationBuilder app)
        {
            var store = DocumentStore.For(Configuration.GetConnectionString("PostgreSql"));
            store.Advanced.Clean.CompletelyRemoveAll();
            using (var session = store.LightweightSession())
            {
                if (!session.Query<IdentityServer4.Postgresql.Entities.ApiResource>().Any())
                {
                    var resources = new List<IdentityServer4.Postgresql.Entities.ApiResource>
                    {
                        new IdentityServer4.Postgresql.Entities.ApiResource
                        {
                            Name = "api1",
                            Description = "Api",
                            DisplayName = "api1",
                            Scopes = new List<ApiScope> {new ApiScope {Name = "api1", DisplayName = "api1"}}
                        },
                    };
                    session.StoreObjects(resources);
                }

                if (!session.Query<IdentityServer4.Postgresql.Entities.IdentityResource>().Any())
                {
                    var resources = new List<IdentityServer4.Postgresql.Entities.IdentityResource>
                    {
                        new IdentityResources.OpenId().ToEntity(),
                        new IdentityResources.Profile().ToEntity(),
                        new IdentityResources.Email().ToEntity(),
                        new IdentityResources.Phone().ToEntity()
                    };
                    session.StoreObjects(resources);
                }
                if (!session.Query<IdentityServer4.Postgresql.Entities.Client>().Any())
                {
                    var clients = new List<IdentityServer4.Postgresql.Entities.Client>
                    {
                        new IdentityServer4.Postgresql.Entities.Client
                        {
                            Id = "ro.client",
                            ClientId = "ro.client",
                            ClientName = "mvc",
                            AllowedGrantTypes = new List<ClientGrantType>
                            {
                                new ClientGrantType {GrantType = GrantType.Hybrid},
                                new ClientGrantType {GrantType = GrantType.ClientCredentials}
                            },
                            AllowedCorsOrigins =
                                new List<ClientCorsOrigin> {new ClientCorsOrigin {Origin = "http://localhost:5000"}},
                            RequireClientSecret = true,
                            ClientSecrets = new List<ClientSecret> {new ClientSecret {Value = "secret".Sha256()}},
                            RequireConsent = false,
                            AllowedScopes = new List<ClientScope>
                            {
                                new ClientScope {Scope = IdentityServer4.IdentityServerConstants.StandardScopes.OpenId},
                                new ClientScope
                                {
                                    Scope = IdentityServer4.IdentityServerConstants.StandardScopes.Profile
                                },
                                new ClientScope {Scope = "api1"}
                            },
                            RedirectUris = new List<ClientRedirectUri>
                            {
                                new ClientRedirectUri {RedirectUri = "http://localhost:5000/signin-oidc"}
                            }
                        }
                    };
                    session.StoreObjects(clients);
                }
                session.SaveChanges();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitData(app);

            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            // middleware for google authentication
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com",
                ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}