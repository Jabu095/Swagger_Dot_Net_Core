using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swagger_Dot_Net_Core
{
    public class SwaggerConfigModule
    {
        public static void Register(IApplicationBuilder app, bool isSwaggerConfig = false, IApiVersionDescriptionProvider provider = null)
        {
            if (isSwaggerConfig)
            {
                app.UseSwagger();
                if (provider == null)
                {
                    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Wrapper V1");
                    });
                }
                else
                {
                    app.UseSwaggerUI(c =>
                    {
                        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                        {
                            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        }
                    });
                }
            }
        }
    }
}
