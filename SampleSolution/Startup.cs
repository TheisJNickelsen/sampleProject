using Marten;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using SampleSolution.Auth;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Common.Domain.Queries;
using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Commands.CommandHandlers;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.Events.EventHandlers;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Helpers;
using SampleSolution.Models;
using SampleSolution.Models.Facebook;
using SampleSolution.Repositories;
using SampleSolution.Services;
using System;
using System.Reflection;
using System.Text;
using Nest;
using SampleSolution.Models.ElasticSearch;

namespace SampleSolution
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSignalR(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureElasticSearch(services);

            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            ConfigureEntityFramework(services);
            
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            ConfigureEventStoring(services);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SomeDataContext>()
                .AddDefaultTokenProviders();

            ConfigureFacebookAuth(services);

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //Configure Jwt
            ConfigureJwtOptions(services);

            //ConfigureCQRS(services);
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            services.AddScoped<ISomeDataReadService, SomeDataReadService>();

            services.AddScoped<ISomeDataRepository, SomeDataRepository>();
            services.AddScoped<IUserStreamReadRepository, UserStreamReadRepository>();
            services.AddScoped<IUserStreamWriteRepository, UserStreamWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBusinessUserRepositoy, BusinessUserRepository>();

            services.AddScoped<ICommandHandler<CreateSomeDataCommand>, SomeDataCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateSomeDataCommand>, SomeDataCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteSomeDataCommand>, SomeDataCommandHandler>();
            services.AddScoped<IEventHandler<SomeDataCreatedEvent>, SomeDataEventHandler>();

            services.AddScoped<ICommandHandler<CreateBusinessUserCommand>, BusinessUserCommandHandler>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<SomeDataContext>(options => options.UseSqlServer(
                Configuration.GetSection("SomeDataContextConnection:ConnectionString").Value, 
                b => b.MigrationsAssembly("SampleSolution.Data")));
        }

        private void ConfigureElasticSearch(IServiceCollection services)
        {
            var settings =
                new ConnectionSettings(new Uri(Configuration.GetSection("ElasticSearchConnection:ConnectionString")
                        .Value))
                    .DefaultIndex("default-index")
                    .DefaultMappingFor<UserStream>(m => m
                        .IndexName("user-stream"));

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
        }

        private void ConfigureSignalR(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200");
            }));

            services.AddSignalR();
        }

        private void ConfigureJwtOptions(IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            var builder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;

                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                o.Lockout.MaxFailedAccessAttempts = 10;
                o.Lockout.AllowedForNewUsers = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<SomeDataContext>().AddDefaultTokenProviders();
        }
        
        private void ConfigureFacebookAuth(IServiceCollection services)
        {
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
        }

        private void ConfigureEventStoring(IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var documentStore = DocumentStore.For(options =>
                {
                    var config = Configuration.GetSection("EventStore");
                    var connectionString = config.GetValue<string>("ConnectionString");
                    var schemaName = config.GetValue<string>("Schema");

                    options.Connection(connectionString);
                    options.AutoCreateSchemaObjects = AutoCreate.None;
                    options.Events.DatabaseSchemaName = schemaName;
                    options.DatabaseSchemaName = schemaName;

                    options.Events.AddEventType(typeof(SomeDataCreatedEvent));
                });

                return documentStore.OpenSession();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
