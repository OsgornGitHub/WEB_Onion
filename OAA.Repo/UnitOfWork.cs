using OAA.Data;
using OAA.Repo.Intarfaces;
using OAA.Repo.Repositories;
using OAA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private Repository<Artist> artistRepository;
        private Repository<Album> albumRepository;
        private Repository<Similar> similarRepository;
        private Repository<Track> trackRepository;

        public UnitOfWork(ApplicationContext context)
        {
            db = context;
        }
        public IRepository<Artist> Artists
        {
            get
            {
                if (artistRepository == null)
                    artistRepository = new Repository<Artist>(db);
                return artistRepository;
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                if (albumRepository == null)
                    albumRepository = new Repository<Album>(db);
                return albumRepository;
            }
        }

        public IRepository<Track> Tracks
        {
            get
            {
                if (trackRepository == null)
                    trackRepository = new Repository<Track>(db);
                return trackRepository;
            }
        }

        public IRepository<Similar> Similars
        {
            get
            {
                if (similarRepository == null)
                    similarRepository = new Repository<Similar>(db);
                return similarRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
