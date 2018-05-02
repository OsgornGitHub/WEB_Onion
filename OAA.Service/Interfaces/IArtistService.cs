using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<Artist> GetAll();
        void Create(Artist artist);
        void Update(Artist artist);
        void Delete(Artist artist);
        Artist Get(string name);
        List<Artist> GetNextPage(int page, int count);
        JObject GetResponse(string url, int page, int count);
        Artist GetArtist(string name);
        JObject GetResponse(string url, string name);
        int GetCountPageTopArtist(int page, int count);
    }
}
