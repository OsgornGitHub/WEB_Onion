using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;

namespace OAA.Repo.Repositories
{
    public class SimilarRepository : IRepository<Similar>
    {
        private ApplicationContext db;
        private DbSet<Similar> entities;

        public SimilarRepository(ApplicationContext context)
        {
            this.db = context;
            entities = db.Set<Similar>();
        }

        public IEnumerable<Similar> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Similar Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.SimilarId == id);
        }

        public void Create(Similar similar)
        {
            if (similar == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(similar);
            db.SaveChanges();
        }

        public void Update(Similar similar)
        {
            if (similar == null)
            {
                throw new ArgumentNullException("entity");
            }
            db.SaveChanges();
        }

        public void Delete(Similar similar)
        {
            if (similar == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(similar);
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
