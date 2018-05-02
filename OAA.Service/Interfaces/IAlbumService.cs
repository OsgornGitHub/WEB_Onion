using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface IAlbumService
    {
        IEnumerable<Album> GetAll();
        void Create(Album album);
        void Update(Album album);
        void Delete(Album album);
        Album Get(string name);
        List<Album> GetTopAlbum(string name, int page, int count);
        bool IsValidAlbum(string cover, string name);
        JObject GetResponse(string url, string name, int page, int count);
        Album GetAlbum(string nameArtist, string nameAlbum);
        JObject GetResponse(string url, string nameArtist, string nameAlbum);
    }
}
