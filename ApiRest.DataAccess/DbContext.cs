using ApiRest.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiRest.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class,IEntity
    {
        //IList<T> _data; //Lista por el momento para hacer pruebas
        //Luego puede ser una conexion a BD o EF;

        DbSet<T> _items;

        ApiDbContext _ctx; //Inyeccion del DbContext

        //constructor
        public DbContext(ApiDbContext ctx)
        {
            _ctx = ctx;
            _items = ctx.Set<T>();
        }


        public void Delete(int id)
        {
            
        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Where(i => i.id.Equals(id)).FirstOrDefault();
        }

        public T Save(T entity)
        {
            _items.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

    }//Fin de la clase DbContext

}//Fin del manespace
