using ApiRest.Abstraction;
using ApiRest.Application;
using ApiRest.DataAccess;
using ApiRest.Repository;
using ApiRest.Services;
using ApiRest.WebApi.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.WebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiRest.WebApi", Version = "v1" });
            });

            //Servicios para agregar Inyeccion de Dependencias

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //Le indicamos al contenedor que cuando pidamos una interface de IRepository nos dé un objeto instanciado de Repository.

            services.AddScoped(typeof(IApplication<>), typeof(Application<>));
            //Le indicamos al contenedor que cuando pidamos una interface de IApplication nos dé un objeto instanciado de Application.

            services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
            //services.AddSingleton(typeof(IDbContext<>), typeof(DbContext<>));
            //.AddSingleton permite que el GET traiga información que enviamos por POST y que no se borra por ser especifica o generica.

            services.AddScoped(typeof(ITokenHandlerService), typeof(TokenHandlerService));
            //Inyectable por dependencia

            //servicio de Conexion a BD
            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("ApiRest.WebApi")));

            //servicio JWT
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            //Servicio Autentificación
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt => {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };

                });

            //servicio Identity para autorización
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApiDbContext>();

            //CORS(Intercambio de recursos de origen cruzado) para poder acceder desde el front end con otro dominio.
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiRest.WebApi v1"));
            }

            //app.UseHttpsRedirection();
            db.Database.Migrate();
            app.UseRouting();

            app.UseCors(options => options
                                             //React                //Propio
                .WithOrigins(new[] {"http://localhost:3000", "http://localhost:8080" , 
                    "http://localhost:4200" }) // Angular
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

            app.UseAuthorization();
            app.UseAuthentication(); //agregado

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
