using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swagger_Dot_Net_Core
{
    public class SwaggerServiceModule
    {
        public static void Register(IServiceCollection services, SwaggerIntegrationHelper swaggerIntegration)
        {
            services.AddMemoryCache();
            if (!string.IsNullOrEmpty(swaggerIntegration.xmlPath))
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = swaggerIntegration.Title, Version = swaggerIntegration.Version, Description = swaggerIntegration.Description, Contact = new OpenApiContact { Email = swaggerIntegration.Email } });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                    c.IncludeXmlComments(swaggerIntegration.xmlPath);
                    c.OperationFilter<SwaggerFileOperationFilter>();
                });
            }

            if (!string.IsNullOrEmpty(swaggerIntegration.JWTISSUER) && !string.IsNullOrEmpty(swaggerIntegration.JWTKEY))
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;

                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = swaggerIntegration.JWTISSUER,
                            ValidAudience = swaggerIntegration.JWTISSUER,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(swaggerIntegration.JWTKEY))
                        };

                    });
            }

        }
    }
}
