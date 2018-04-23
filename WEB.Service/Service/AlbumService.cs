using System;
using System.Collections.Generic;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;
using WEB.Service.Interfaces;

namespace WEB.Service.Service
{
    public class AlbumService : IAlbumService
    {
        private IRepository<Album> albumRepository;

        public AlbumService(IRepository<Album> albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public Album GetAlbum(Guid id)
        {
            return albumRepository.Get(id);
        }
    }
}
