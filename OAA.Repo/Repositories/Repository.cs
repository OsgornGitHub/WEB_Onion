using Microsoft.EntityFrameworkCore;
using OAA.Data;
using OAA.Data.Models;
using OAA.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAA.Repo.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private ApplicationContext db;
        private DbSet<T> entities;

        public Repository(ApplicationContext context)
        {
            this.db = context;
            entities = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetAlbumByName(string nameArtist)
        {
            var albums = entities.FromSql("EXECUTE dbo.GetAlbumByName {0}", nameArtist);
            return albums;
        }

        public void Create(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(item);
        }

        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(item);
        }

        public void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(item);
        }
    }
}
