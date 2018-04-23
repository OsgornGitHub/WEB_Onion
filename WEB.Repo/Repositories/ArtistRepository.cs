using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;

namespace WEB.Repo.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private ApplicationContext db;

        public ArtistRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Artist> GetAll()
        {
            return db.Artists;
        }

        public Artist Get(Guid id)
        {
            return db.Artists.Find(id);
        }

        public void Create(Artist artist)
        {
            db.Artists.Add(artist);
        }

        public void Update(Artist artist)
        {
            db.Entry(artist).State = EntityState.Modified;
        }

        public IEnumerable<Artist> Find(Guid id)
        {
            return db.Artists.Where(a => a.ArtistId == id).ToList();
        }

        public void Delete(Guid id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist != null)
                db.Artists.Remove(artist);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
