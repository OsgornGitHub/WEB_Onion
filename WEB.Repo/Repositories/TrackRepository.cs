using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;

namespace WEB.Repo.Repositories
{
    public class TrackRepository : IRepository<Track>
    {
        private ApplicationContext db;

        public TrackRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Track> GetAll()
        {
            return db.Tracks;
        }

        public Track Get(Guid id)
        {
            return db.Tracks.Find(id);
        }

        public void Create(Track track)
        {
            db.Tracks.Add(track);
        }

        public void Update(Track track)
        {
            db.Entry(track).State = EntityState.Modified;
        }

        public IEnumerable<Track> Find(Guid id)
        {
            return db.Tracks.Where(a => a.TrackId == id).ToList();
        }

        public void Delete(Guid id)
        {
            Track track = db.Tracks.Find(id);
            if (track != null)
                db.Tracks.Remove(track);
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
