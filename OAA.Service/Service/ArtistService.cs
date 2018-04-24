
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using OAA.Data;
using OAA.Repo.Repositories;
using OAA.Service.Interfaces;

namespace OAA.Service.Service
{
    public class ArtistService : IArtistService
    {
        IUnitOfWork Database { get; set; }

        public ArtistService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<Artist> GetAll()
        {
            return Database.Artists.GetAll();
        }

        public Artist GetFromBd(Guid id)
        {
            return Database.Artists.Get(id);
        }

        public void Create(Artist artist)
        {
            Database.Artists.Create(artist);
            Database.Save();
        }
        public void Update(Artist artist)
        {
            Database.Artists.Update(artist);
            Database.Save();
        }

        public void Delete(Artist artist)
        {
            Database.Artists.Delete(artist);
            Database.Save();
        }
    }
}
