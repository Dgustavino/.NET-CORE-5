using ApiRest.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.DataAccess
{
   public class ApiDbContext: IdentityDbContext
    {
        public DbSet<FootballTeam> Teams { get; set; }

        //constructor
        public ApiDbContext(DbContextOptions<ApiDbContext> options ) : base(options)
        {

        }

        //Esta operacion permite configurar que queremos que suceda cuando se crea el modelo de base de datos al ejecutar la aplicación y al crear la migración del modelo de objetos al modelo de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>(); //le decimos que ignore la clase Entity
            base.OnModelCreating(modelBuilder);
        }
    }
}
