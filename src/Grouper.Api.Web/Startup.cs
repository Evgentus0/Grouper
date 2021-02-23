using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Infrastructure.Automapper;
using Grouper.Api.Infrastructure.Core;
using Grouper.Api.Infrastructure.Extensions;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Infrastructure.Services;
using Grouper.Api.Infrastructure.Settings;
using Grouper.Api.Web.Automapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grouper.Api.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Settings = Configuration.Get<AppSettings>();
        }

        public IConfiguration Configuration { get; }
        protected ILoggerFactory LoggerFactory;

        protected virtual AppSettings Settings { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Grouper.Api.Web", Version = "v1" });
            });

            services.AddData(Configuration, LoggerFactory, Settings);

            services.AddAutoMapper(config => 
            {
                config.AddProfile(new EntiyDtoMapperProfile());
                config.AddProfile(new DtoModelMapperProfile());
            });

            ConfigureIoC(services);

            ConfgureAuthentication(services);
        }

        protected virtual void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton(Settings);

            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped(x => new JwtSecurityTokenHandler());
        }

        protected virtual void ConfgureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Authorization.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new 
                            {
                                Exception = context.Exception,
                                StatusCode = HttpStatusCode.Unauthorized
                            }));

                            return Task.FromResult(0);
                        },
                        OnChallenge = context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                // Override the response status code.
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                                // Emit the WWW-Authenticate header.
                                context.Response.Headers.Append(HeaderNames.WWWAuthenticate, context.Options.Challenge);

                                if (!string.IsNullOrEmpty(context.Error))
                                {
                                    context.Response.WriteAsync(context.Error);
                                }

                                if (!string.IsNullOrEmpty(context.ErrorDescription))
                                {
                                    context.Response.WriteAsync(context.ErrorDescription);
                                }
                            }
                            context.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.CreateAndInitIfNotExist();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grouper.Api.Web v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
