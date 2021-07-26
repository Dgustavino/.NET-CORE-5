using ApiRest.Abstraction;
using System;
using System.Collections.Generic;

namespace ApiRest.Repository
{
    public interface IRepository<T>: ICrud<T>
    {

    }
    public class Repository<T> : IRepository<T> where T : IEntity
    {

        //Inyectamos el DbContext
        IDbContext<T> _ctx;

        //constructor
        public Repository(IDbContext<T> ctx)
        {
            _ctx = ctx;
        }

        public void Delete(int id)
        {
            _ctx.Delete(id);
        }

        public IList<T> GetAll()
        {
            return _ctx.GetAll();
        }

        public T GetById(int id)
        {
            return _ctx.GetById(id);
        }

        public T Save(T entity)
        {
            return _ctx.Save(entity);
        }
    }
}
