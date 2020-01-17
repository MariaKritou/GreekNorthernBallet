using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQW;
using SQW.Interfaces;


namespace GreekNorthernBallet
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

      var databaseOptionsSection = Configuration.GetSection("DatabaseOptions");
      services.Configure<DatabaseOptions>(databaseOptionsSection);
      var databaseOptions = databaseOptionsSection.Get<DatabaseOptions>();

      var oraConfig = new SQWOraConfig
      {
        host = databaseOptions.dbServer,
        instance = databaseOptions.dbInstance,
        userName = databaseOptions.userName,
        password = databaseOptions.password
      };

      var sequenceGenerator = new SQWSequenceGenerator();

      SQWOraWorker worker = new SQWOraWorker(oraConfig, sequenceGenerator);
      services.AddSingleton<ISQWWorker>(worker);

      services.Configure<CookiePolicyOptions>(options =>
      {
              // This lambda determines whether user consent for non-essential cookies is needed for a given request.
              options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });


      services.AddDistributedMemoryCache();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddSingleton<NewsDatabaseRepository>();
      services.AddSingleton<SchedulerDatabaseRepository>();
      services.AddSingleton<EventDatabaseRepository>();
      services.AddSingleton<UserDatabaseRepository>();
      services.AddSingleton<PaymentsDatabaseRepository>();
      //προσθεσα αυτη για να βρισκω το current user id --svhseee
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


      //προσθεσα αυτη για αυθεντικοποιηση
      services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
          options.LoginPath = "/User/Login";
          options.AccessDeniedPath = "/User/UnauthorizedView";
        });
      //προσθεσα για εξουσιοδότιση
      services.AddAuthorization(options =>
      {
        options.AddPolicy("admin", policy => policy.RequireRole("admin"));
        options.AddPolicy("user", policy => policy.RequireRole("user"));
      });
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
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();
      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
