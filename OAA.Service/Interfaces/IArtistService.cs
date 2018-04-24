using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<Artist> GetAll();
        Artist GetFromBd(Guid id);
        void Create(Artist artist);
        void Update(Artist artist);
        void Delete(Artist artist);
    }
}
