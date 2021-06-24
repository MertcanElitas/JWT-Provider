using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithToken.Domain.Context;
using ApiWithToken.Domain.Repositories.Concrete;
using ApiWithToken.Domain.Repositories.Interfaces;
using ApiWithToken.Domain.Repositories.UnitOfWork;
using ApiWithToken.Domain.Services;
using ApiWithToken.Responses;
using ApiWithToken.Security.Token;
using ApiWithToken.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiWithToken
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
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(configure =>
                {
                    configure.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    //Uygulamamıza gelen isteğin, appsetting.json kısmında tanımladığımız audience değerindenki adresden geldiğini kontrol et demiş olduk true diyerek.
                    //Clientı doğrula.
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,


                    //Servar name kontrol et.
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,

                    //Tokenın expire süresini kontrol et.Bakalım geçmişmi.
                    ValidateLifetime = true,

                    //Gelen requestin SecurityKey değerini doğrula
                    ValidateIssuerSigningKey = true,


                    //Bizim tanımladığımız securityKey değerine göre kontrol yapmasını belirttik.
                    IssuerSigningKey = SignHandle.GetSecurityKey(tokenOptions.SecurityKey),

                    //Tokenı oluşuturucak sunucular arasında zaman farkı varsa o farklı tölera etmek için kullanılır.
                    //Uygulamanın bir kopyasının amerikada bi kopyasının türkiyede olduğunu düşünürsek aradaki saat farkı 1 saat ise
                    //ClockSkew değeri 1 saat ayarlanarak tölera edliir.
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddDbContext<ApiWihTokenDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
