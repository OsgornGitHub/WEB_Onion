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
        private ArtistRepository artistRepository;
        private AlbumRepository albumRepository;
        private SimilarRepository similarRepository;
        private TrackRepository trackRepository;

        public UnitOfWork(ApplicationContext context)
        {
            db = context;
        }
        public IRepository<Artist> Artists
        {
            get
            {
                if (artistRepository == null)
                    artistRepository = new ArtistRepository(db);
                return artistRepository;
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                if (albumRepository == null)
                    albumRepository = new AlbumRepository(db);
                return albumRepository;
            }
        }

        public IRepository<Track> Tracks
        {
            get
            {
                if (trackRepository == null)
                    trackRepository = new TrackRepository(db);
                return trackRepository;
            }
        }

        public IRepository<Similar> Similars
        {
            get
            {
                if (similarRepository == null)
                    similarRepository = new SimilarRepository(db);
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
