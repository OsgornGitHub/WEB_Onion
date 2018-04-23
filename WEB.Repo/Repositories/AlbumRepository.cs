using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;

namespace WEB.Repo.Repositories
{
    public class AlbumRepository : IRepository<Album>
    {
        private ApplicationContext db;

        public AlbumRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Album> GetAll()
        {
            return db.Albums;
        }

        public Album Get(Guid id)
        {
            return db.Albums.Find(id);
        }

        public void Create(Album album)
        {
            db.Albums.Add(album);
        }

        public void Update(Album album)
        {
            db.Entry(album).State = EntityState.Modified;
        }

        public IEnumerable<Album> Find(Guid id)
        {
            return db.Albums.Where(a => a.AlbumId == id).ToList();
        }

        public void Delete(Guid id)
        {
            Album album = db.Albums.Find(id);
            if (album != null)
                db.Albums.Remove(album);
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
