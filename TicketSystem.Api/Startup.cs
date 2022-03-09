using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using TicketSystem.Common.Authorizations;
using TicketSystem.Common.AutoInjection;
using TicketSystem.Common.AutoMapper;
using TicketSystem.Common.Filters;
using TicketSystem.Common.Middlewares;
using TicketSystem.Common.Swagger;

namespace TicketSystem.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalPermissionFilter));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddAutoInjection();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddSwagger();


            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    // TODO: Get from appsettings
                    var secret = "AnAppleADayKeepsTheDoctorAway";
                    var issuer = "TicketSystem";
                    var audience = "TicketSystem.Api";

                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        RoleClaimType = JwtClaimTypes.Role,
                        NameClaimType = JwtClaimTypes.Name,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                    };
                });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
