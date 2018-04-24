using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;

namespace OAA.Repo.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private ApplicationContext db;
        private DbSet<Artist> entities;

        public ArtistRepository(ApplicationContext context)
        {
            this.db = context;
            entities = db.Set<Artist>();
        }

        public IEnumerable<Artist> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Artist Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.ArtistId == id);
        }

        public void Create(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(artist);
            db.SaveChanges();
        }

        public void Update(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException("entity");
            }
            db.SaveChanges();
        }

        public void Delete(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(artist);
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
