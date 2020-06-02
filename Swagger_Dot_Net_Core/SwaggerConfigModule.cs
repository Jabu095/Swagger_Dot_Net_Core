using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swagger_Dot_Net_Core
{
    public class SwaggerConfigModule
    {
        public static void Register(IApplicationBuilder app, bool isSwaggerConfig = false)
        {
            if (isSwaggerConfig)
            {
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Wrapper V1");
                });
            }
        }
    }
}
