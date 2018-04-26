﻿using System;
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
            var cover = "";
            foreach (var music in ResultJson.toptracks.track)
            {
                nameTrack = music.name;
                foreach (dynamic dyn in music.image)
                {
                    if (dyn.size == "extralarge")
                    {
                        cover = dyn.text;
                        break;
                    }
                }
                Track track = new Track()
                {
                    TrackId = Guid.NewGuid(),
                    Name = nameTrack,
                    Cover = cover
                };
                topTracks.Add(track);
            }
            return topTracks;
        }

        public void AddTrackFromLast(string nameTrack, string nameArtist, string link)
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=track.getInfo&artist=" + nameArtist + "&track=" + nameTrack + "&api_key=" + "1068375741deac644574d04838a37810" + "&format=json");
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string Result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            dynamic ResultJson = JObject.Parse(Result);
            var cover = "";
            foreach (dynamic dyn in ResultJson.track.album.image)
            {
                if (dyn.size == "extralarge")
                {
                    cover = dyn.text;
                    break;
                }
            }
            Track track = new Track()
            {
                TrackId = Guid.NewGuid(),
                Name = ResultJson.track.name,
                Cover = cover,
                Link = link
            };
            Create(track);
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
    }
}
