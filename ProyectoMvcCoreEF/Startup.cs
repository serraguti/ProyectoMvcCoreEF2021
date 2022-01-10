using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoMvcCoreEF.Data;
using ProyectoMvcCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMvcCoreEF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            String cadenaMySQL =
                this.Configuration.GetConnectionString("cadenahospitalmysql");
            DepartamentosContextMySql contextMySql =
                new DepartamentosContextMySql(cadenaMySQL);
            services.AddTransient<IDepartamentosContext>
                (x => contextMySql);
            //String cadenaSqlServer =
            //    this.Configuration.GetConnectionString("cadenahospitalsql");
            //DepartamentosContextSQLServer contextSQL =
            //    new DepartamentosContextSQLServer(cadenaSqlServer);
            //services.AddTransient<IDepartamentosContext>
            //    (z => contextSQL);



            //DEBEMOS RESOLVER LAS DEPENDENCIAS ANTES DE 
            //CARGAR LOS CONTROLADORES EN LOS SERVICIOS
            //services.AddSingleton<ICoche, Deportivo>();
            //VAMOS A INSTANCIAR UN OBJETO Y LO ENVIAREMOS CREADO
            //DESDE AQUI
            Deportivo deportivo = new Deportivo();
            deportivo.Marca = "Furgoneta";
            deportivo.Modelo = "Scoobyeeeeee Doo";
            deportivo.Imagen = "coche1.jpg";
            deportivo.VelocidadMaxima = 210;
            services.AddSingleton<ICoche>(x => deportivo);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
