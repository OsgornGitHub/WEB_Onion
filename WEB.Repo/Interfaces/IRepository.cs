﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Repo.Intarfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        IEnumerable<T> Find(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        void SaveChanges();
    }
}