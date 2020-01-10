using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                    c.SwaggerDoc("v1", new Info { Title = swaggerIntegration.Title, Version = swaggerIntegration.Version, Description = swaggerIntegration.Description, Contact = new Contact() { Email = swaggerIntegration.Email } });
                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                 {
                        { "Bearer", new string[]{ } },
                        { "Basic", new string[]{ } },
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
