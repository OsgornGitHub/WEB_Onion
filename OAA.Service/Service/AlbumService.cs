using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;
using OAA.Service.Interfaces;

namespace OAA.Service.Service
{
    public class AlbumService : IAlbumService
    {
        private IRepository<Album> albumRepository;

        IUnitOfWork Database { get; set; }

        public AlbumService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Album GetAlbum(Guid id)
        {
            return Database.Albums.Get(id);
        }
    }
}
