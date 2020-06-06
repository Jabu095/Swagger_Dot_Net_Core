Swagger_Dot_Net_Core

Swagger api documentation and JWT integration for C# .NET CORE projects,
This project can be used to setup swagger documentation for your .NET CORE WEBAPI Applications the package incluids the JWT swagger intergration and API versioning

Example usage:
1. Install the standard Nuget package into your ASP.NET Core application.

  Install-Package Swagger_Dot_Net_Core -Version 1.0.7
  dotnet add package Swagger_Dot_Net_Core --version 1.0.7
  <PackageReference Include="Swagger_Dot_Net_Core" Version="1.0.7" />
  paket add Swagger_Dot_Net_Core --version 1.0.7
  
2. Add the following code on Startup.cs class
```csharp

public void ConfigureServices(IServiceCollection services)
{
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  Swagger_Dot_Net_Core.SwaggerServiceModule.Register(services, new Swagger_Dot_Net_Core.SwaggerIntegrationHelper 
  {
    Description = "Your api",
    Email = "user@gmail.com",
    JWTISSUER = "jwtissuer",
    JWTKEY = "jwtkey",
    Title = "Api",
    Version = "V1",
    xmlPath = xmlPath
   });
  services.AddControllers();
  services.AddApiVersioning(
               options =>
               {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
               });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env,IApiVersionDescriptionProvider provider)
{
  if (env.IsDevelopment())
  {
     app.UseDeveloperExceptionPage();
  }
  else
  {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-  hsts.
    app.UseHsts();
  }

  app.UseHttpsRedirection();
  Swagger_Dot_Net_Core.SwaggerConfigModule.Register(app, true,provider);
  app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
}
  
