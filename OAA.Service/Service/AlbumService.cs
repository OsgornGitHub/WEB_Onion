using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using OAA.Data;
using OAA.Service.Interfaces;

namespace OAA.Service.Service
{
    public class AlbumService : IAlbumService
    {
        IUnitOfWork Database { get; set; }

        public AlbumService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<Album> GetAll()
        {
            return Database.Albums.GetAll();
        }

        public Album Get(string name)
        {
            return Database.Albums.GetAll().FirstOrDefault(a => a.Name == name);
        }

        public Album GetById(Guid id)
        {
            return Database.Albums.GetAll().FirstOrDefault(a => a.Id == id);
        }

        public void Create(Album album)
        {
            Database.Albums.Create(album);
            Database.Save();
        }
        public void Update(Album album)
        {
            Database.Albums.Update(album);
            Database.Save();
        }

        public void Delete(Album album)
        {
            Database.Albums.Delete(album);
            Database.Save();
        }

        public List<Album> GetTopAlbum(string name, int page, int count)
        {
            List<Album> topAlbums = new List<Album>();
            var nameAlbum = "";
            var cover = "";
            dynamic ResultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=", name, page, count);
            foreach (var tr in ResultJson.topalbums.album)
            {
                nameAlbum = tr.name;
                foreach (dynamic dyn in tr.image)
                {
                    if (dyn.size == "extralarge")
                    {
                        cover = dyn.text;
                        break;
                    }
                }
                if (IsValidAlbum(cover, nameAlbum))
                {
                    Album album = new Album()
                    {
                        Id = Guid.NewGuid(),
                        Name = nameAlbum,
                        NameArtist = name.Replace("+", " "),
                        Cover = cover
                    };
                    topAlbums.Add(album);

                }
            }
            return topAlbums;
        }

        public bool IsValidAlbum(string cover, string name)
        {
            if (name == "null" || cover == "null" || cover == "" || name == "")
            {
                return false;
            }
            return true;
        }

        public JObject GetResponse(string url, string name, int page, int count)
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(url + name + "&api_key=" + "1068375741deac644574d04838a37810" + "&limit=" + count + "&page=" + page + "&format=json");
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            result = result.Replace("#", "");
            JObject resultJson = JObject.Parse(result);
            return resultJson;
        }

        public Album GetAlbum(string nameArtist, string nameAlbum)
        {
            dynamic ResultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&artist=", nameArtist, nameAlbum);
            List<Track> tracks = new List<Track>();
            var albumId = Guid.NewGuid();
            foreach (var tr in ResultJson.album.tracks.track)
            {
                Track track = new Track()
                {
                    Name = tr.name,
                    NameAlbum = nameAlbum
                };
                tracks.Add(track);
            }
            string artistName = ResultJson.album.artist;
            var image = "";
            foreach (dynamic dyn in ResultJson.album.image)
            {
                if (dyn.size == "mega")
                {
                    image = dyn.text;
                    break;
                }
            }
            Album album = new Album()
            {
                Name = nameAlbum,
                NameArtist = artistName,
                Cover = image,
                Tracks = tracks
            };

            return album;
        }

        public JObject GetResponse(string url, string nameArtist, string nameAlbum)
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(url + nameArtist + "&album=" + nameAlbum + "&api_key=" + "1068375741deac644574d04838a37810" + "&format=json");
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            result = result.Replace("#", "");
            JObject resultJson = JObject.Parse(result);
            return resultJson;
        }

    }
}
