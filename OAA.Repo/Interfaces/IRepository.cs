using OAA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Repo.Intarfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
