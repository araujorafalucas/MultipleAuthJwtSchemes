using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace MultipleAuthSchemes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddJwtBearer("AlphaClient", options =>
                                                      {
                                                          options.TokenValidationParameters = new TokenValidationParameters()
                                                          {
                                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myunlegiveblealphasecret")),
                                                              ValidAudience = "AudienceClientAlpha",
                                                              ValidIssuer = "IssuerClientAlpha",
                                                              ValidateIssuerSigningKey = true,
                                                              ValidateLifetime = true,
                                                              ClockSkew = TimeSpan.Zero
                                                          };
                                                      
                                                      })
                                         .AddJwtBearer("BetaClient", options =>
                                         {
                                             options.TokenValidationParameters = new TokenValidationParameters()
                                             {
                                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myunlegiveblebetasecret")),
                                                 ValidAudience = "AudienceClientBeta",
                                                 ValidIssuer = "IssuerClientBeta",
                                                 ValidateIssuerSigningKey = true,
                                                 ValidateLifetime = true,
                                                 ClockSkew = TimeSpan.Zero
                                             };
                                         });
            services.AddAuthorization();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
