using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;

namespace OAA.Repo.Repositories
{
    public class AlbumRepository : IRepository<Album>
    {
        private ApplicationContext db;
        private DbSet<Album> entities;

        public AlbumRepository(ApplicationContext context)
        {
            this.db = context;
            entities = db.Set<Album>();
        }

        public IEnumerable<Album> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Album Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.AlbumId == id);
        }

        public void Create(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(album);
            db.SaveChanges();
        }

        public void Update(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("entity");
            }
            db.SaveChanges();
        }

        public void Delete(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(album);
            db.SaveChanges();
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
