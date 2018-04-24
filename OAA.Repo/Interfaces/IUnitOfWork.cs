using OAA.Data;
using OAA.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Service.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Artist> Artists { get; }
        IRepository<Album> Albums { get; }
        IRepository<Similar> Similars { get; }
        IRepository<Track> Tracks { get; }
        void Save();
    }
}
