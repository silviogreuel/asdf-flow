using System.IO;
using System.Linq;
using System.Text;
using Asdf.Application.Api.Auth;
using Asdf.Application.Database;
using Asdf.Kernel.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Configuration.BasicPublish;
using RawRabbit.Instantiation;
using Swashbuckle.AspNetCore.Swagger;

namespace Asdf.Application.Api
{
    public static class Env
    {
        public static string Name()
        {
#if (DEBUG)
            return "Debug";
#else
            return "Release";
#endif
        }
    }

    public class Startup
    {
        public static RawRabbit.Instantiation.Disposable.BusClient _bus;
            
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"rawrabbit.{Env.Name()}.json")
                .Build()
                .Get<RawRabbitConfiguration>();

            var rabbitOptions = new RawRabbitOptions()
            {
                ClientConfiguration = configuration
            };

            _bus = RawRabbitFactory.CreateSingleton(rabbitOptions);

             GlobalBus.Publish = async (exchange, routing, body) =>
            {
                await _bus.BasicPublishAsync(new BasicPublishConfiguration()
                {
                    ExchangeName = exchange,
                    RoutingKey = routing,
                    Body =  body
                });
            };

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors();
            services.AddDbContext<AsdfContext>();

            services
                .AddAuthentication(options =>
                {
                    //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultAuthenticateScheme = "smart";
                    options.DefaultChallengeScheme = "smart";
                })
                .AddPolicyScheme("smart", "JWT or OAUTH", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (authHeader?.StartsWith("Bearer ") == true)
                        {
                            return JwtBearerDefaults.AuthenticationScheme;
                        }

                        return CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                })
                .AddFacebook(options =>
                {
                    options.AppId = "2264103893631941";
                    options.AppSecret = "6d275e5f00239e5ee19c3dfdeb92ff2a";
                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "Q1m5BOi1qILK4NsuFKNoHSKUY";
                    options.ConsumerSecret = "xG569ghrO0RTQZaobNUpTEWQzmgqncualVehnLiH3hjKyPTUlx";
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TLAYa3jcU8Hg8r6BEE2yY2vzrZLUO4rc")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                })
                .AddCookie(options =>
                {
                    //options.LoginPath = "auth/signin/Twitter";
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.Domain = "localhost";
                });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddScoped<IAuthService, AuthService>();
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
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asdf API V1");
            });
            app.UseCors(c => c
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());

            app.UseMvc();
        }
    }
}
