using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;

namespace WEB.Repo.Repositories
{
    public class SimilarRepository : IRepository<Similar>
    {
        private ApplicationContext db;

        public SimilarRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Similar> GetAll()
        {
            return db.Similars;
        }

        public Similar Get(Guid id)
        {
            return db.Similars.Find(id);
        }

        public void Create(Similar similar)
        {
            db.Similars.Add(similar);
        }

        public void Update(Similar similar)
        {
            db.Entry(similar).State = EntityState.Modified;
        }

        public IEnumerable<Similar> Find(Guid id)
        {
            return db.Similars.Where(a => a.SimilarId == id).ToList();
        }

        public void Delete(Guid id)
        {
            Similar similar = db.Similars.Find(id);
            if (similar != null)
                db.Similars.Remove(similar);
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
