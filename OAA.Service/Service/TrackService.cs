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
    public class TrackService : ITrackService
    {
        IUnitOfWork Database { get; set; }

        public TrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<Track> GetAll()
        {
            return Database.Tracks.GetAll();
        }

        public void Create(Track track)
        {
            Database.Tracks.Create(track);
            Database.Save();
        }
        public void Update(Track track)
        {
            Database.Tracks.Update(track);
            Database.Save();
        }

        public void Delete(Track track)
        {
            Database.Tracks.Delete(track);
            Database.Save();
        }

        public List<Track> GetTopTracks(string name, int count = 24, int page = 1)
        {
            List<Track> topTracks = new List<Track>();
            dynamic ResultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=", name, page, count);
            var nameTrack = "";
            foreach (var music in ResultJson.toptracks.track)
            {
                nameTrack = music.name;
                Track track = new Track()
                {
                    TrackId = Guid.NewGuid(),
                    Name = nameTrack,
                };
                topTracks.Add(track);
            }
            return topTracks;
        }

        public Track AddTrackFromLast(string nameTrack, string nameArtist, string link)
        {
            dynamic resultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=track.getInfo", nameTrack, nameArtist.Replace(" ","+"));
            Track track = new Track()
            {
                TrackId = Guid.NewGuid(),
                Name = resultJson.track.name,
                NameAlbum = GetAlbumTrackName(nameArtist, nameTrack),
                Link = link
            };
            return track;
        }

        public string GetAlbumTrackName(string nameArtist, string nameTrack)
        {
            dynamic resultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=track.getInfo", nameTrack, nameArtist);
            string nameAlbum = resultJson.track.album.title;
            return nameAlbum;
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

        public JObject GetResponse(string url, string nameTrack, string nameArtist)
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(url + "&artist=" + nameArtist + "&track=" + nameTrack + "&api_key=" + "1068375741deac644574d04838a37810" + "&format=json");
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            result = result.Replace("#", "");
            JObject resultJson = JObject.Parse(result);
            return resultJson;
        }

    }
}
