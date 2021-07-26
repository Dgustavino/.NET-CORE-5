using ApiRest.Abstraction;
using ApiRest.Repository;
using System;
using System.Collections.Generic;

namespace ApiRest.Application
{
    public interface IApplication<T>: ICrud<T>
    {

    }

    public class Application<T> : IApplication<T> where T: IEntity
    {
        //Inyeccion de Dependencias
        IRepository<T> _repository; 

        //Constructor
        public Application(IRepository<T> repository) //Inversion de Control
        {
            _repository = repository;
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T Save(T entity)
        {
            return _repository.Save(entity);
        }
    }
}
