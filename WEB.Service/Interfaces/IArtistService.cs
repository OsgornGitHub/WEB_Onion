using System;
using System.Collections.Generic;
using System.Text;
using WEB.Data;

namespace WEB.Service.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<Artist> GetAll();
        Artist GetFromBd(Guid id);
        void Create(Artist artist);
        void Update(Artist artist);
        void Delete(Guid id);
    }
}
