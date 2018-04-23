using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using WEB.Data;
using WEB.Repo.Repositories;
using WEB.Service.Interfaces;

namespace WEB.Service.Service
{
    public class ArtistService : IArtistService
    {
        private ArtistRepository artistRepository;
        //private IRepository<UserProfile> userProfileRepository;

        public ArtistService (ArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public IEnumerable<Artist> GetAll()
        {
            return artistRepository.GetAll();
        }

        public Artist GetFromBd(Guid id)
        {
            return artistRepository.Get(id);
        }

        public void Create(Artist artist)
        {
            artistRepository.Create(artist);
        }
        public void Update(Artist artist)
        {
            artistRepository.Update(artist);
        }

        public void Delete(Guid id)
        {
            artistRepository.Delete(id);
            artistRepository.SaveChanges();
        }
    }
}
