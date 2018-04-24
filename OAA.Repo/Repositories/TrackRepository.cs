using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;

namespace OAA.Repo.Repositories
{
    public class TrackRepository : IRepository<Track>
    {
        private ApplicationContext db;
        private DbSet<Track> entities;

        public TrackRepository(ApplicationContext context)
        {
            this.db = context;
            entities = db.Set<Track>();
        }

        public IEnumerable<Track> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Track Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.TrackId == id);
        }

        public void Create(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(track);
            db.SaveChanges();
        }

        public void Update(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException("entity");
            }
            db.SaveChanges();
        }

        public void Delete(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(track);
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
