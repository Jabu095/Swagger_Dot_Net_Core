Swagger_Dot_Net_Core

Swagger api documentation and JWT integration for C# .NET CORE 2 and below,
This project can be used to quickly and swagger documentation for your APIS and also incluids the JWT swagger intergration

Example usage:

```csharp
StartUp file

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
  services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
  if (env.IsDevelopment())
  {
     app.UseDeveloperExceptionPage();
  }
  else
  {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
  }

  app.UseHttpsRedirection();
  Swagger_Dot_Net_Core.SwaggerConfigModule.Register(app, true);
  app.UseMvc();
  }
  
