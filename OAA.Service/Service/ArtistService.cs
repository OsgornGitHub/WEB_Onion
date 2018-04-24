
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using OAA.Data;
using OAA.Repo.Repositories;
using OAA.Service.Interfaces;

namespace OAA.Service.Service
{
    public class ArtistService : IArtistService
    {
        IUnitOfWork Database { get; set; }

        public ArtistService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<Artist> GetAll()
        {
            return Database.Artists.GetAll();
        }

        public Artist GetFromBd(Guid id)
        {
            return Database.Artists.Get(id);
        }

        public void Create(Artist artist)
        {
            Database.Artists.Create(artist);
            Database.Save();
        }
        public void Update(Artist artist)
        {
            Database.Artists.Update(artist);
            Database.Save();
        }

        public void Delete(Artist artist)
        {
            Database.Artists.Delete(artist);
            Database.Save();
        }

        public List<Artist> GetNextPage(int page, int count)
        {
            List<Artist> list = new List<Artist>();
            dynamic ResultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&api_key=", page, count);
            foreach (var person in ResultJson.artists.artist)
            {
                string name = person.name;
                string photo = "";
                foreach (dynamic dyn in person.image)
                {
                    if (dyn.size == "mega")
                    {
                        photo = dyn.text;
                        break;
                    }
                }
                Artist artist = new Artist
                {
                    Name = name,
                    Photo = photo
                };
                if (list.Count >= count)
                {
                    break;
                }
                list.Add(artist);
            }
            return list;
        }


        public Artist GetArtist(string name)
        {
            string validName = IsValidName(name);
            dynamic ResultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=", validName);
            string bio = ResultJson.artist.bio.content;

            string photo = "";
            foreach (dynamic dyn in ResultJson.artist.image)
            {
                if (dyn.size == "mega")
                {
                    photo = dyn.text;
                    break;
                }
            }
            Artist artist = new Artist()
            {
                ArtistId = Guid.NewGuid(),
                Name = name,
                Photo = photo,
                Biography = bio
            };
            return artist;
        }

        public string IsValidName(string name)
        {
            var validName = "";
            if (name.IndexOf(" ") != -1)
            {
                var longName = name.Split(" ");
                for (int i = 0; i < longName.Length; i++)
                {
                    if (i != longName.Length - 1)
                    {
                        validName += (longName[i] + "+");
                    }
                    else
                    {
                        validName += longName[i];
                    }

                }
                return validName;
            }
            return name;
        }

        public JObject GetResponse(string url, int page, int count)
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(url + "&limit=" + count + "&page=" + page + "&api_key=" + "1068375741deac644574d04838a37810" + "&format=json");
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string Result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            Result = Result.Replace("#", "");
            dynamic ResultJson = JObject.Parse(Result);
            return ResultJson;
        }

        public JObject GetResponse(string url, string name)
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(url + name + "&api_key=" + "1068375741deac644574d04838a37810" + "&format=json");
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string Result = new StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            Result = Result.Replace("#", "");
            dynamic ResultJson = JObject.Parse(Result);
            return ResultJson;
        }

      
    }
}
