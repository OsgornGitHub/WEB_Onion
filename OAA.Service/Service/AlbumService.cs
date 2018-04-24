using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using OAA.Data;
using OAA.Repo.Intarfaces;
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

        public Album GetFromBd(Guid id)
        {
            return Database.Albums.Get(id);
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
                        AlbumId = Guid.NewGuid(),
                        NameAlbum = nameAlbum,
                        NameArtist = name,
                        Cover = cover
                    };
                    //db.Albums.Add(album);
                    topAlbums.Add(album);
                    //db.SaveChanges();
                }
                //else topAlbums.Add(GetOneTopAlbum(name, page, count));
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
            string Result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            Result = Result.Replace("#", "");
            dynamic ResultJson = JObject.Parse(Result);
            return ResultJson;
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
                    TrackId = Guid.NewGuid(),
                    Name = tr.name
                };
                tracks.Add(track);
            }
            var albumName = ResultJson.album.name;
            var artistName = ResultJson.album.artist;
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
                AlbumId = Guid.NewGuid(),
                NameAlbum = albumName,
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
            string Result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            Result = Result.Replace("#", "");
            dynamic ResultJson = JObject.Parse(Result);
            return ResultJson;
        }

    }
}
