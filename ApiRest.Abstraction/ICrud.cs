using System;
using System.Collections.Generic;

namespace ApiRest.Abstraction
{
    public interface ICrud<T>
    {
        T Save(T entity); //Inserta y/o Actualiza

        IList<T> GetAll(); // Lee, trae, devuelve todo

        T GetById(int id); //devuelve, busca por Id

        void Delete(int id); //Elimina por Id
    }
}
