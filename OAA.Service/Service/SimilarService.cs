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
    public class SimilarService : ISimilarService
    {
        IUnitOfWork Database { get; set; }

        public SimilarService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Similar GetSimilarBd(Guid id)
        {
            return Database.Similars.Get(id);
        }

        public IEnumerable<Similar> GetAll()
        {
            return Database.Similars.GetAll();
        }

        public void Create(Similar similars)
        {
            Database.Similars.Create(similars);
            Database.Save();
        }
        public void Update(Similar similars)
        {
            Database.Similars.Update(similars);
            Database.Save();
        }

        public void Delete(Similar similars)
        {
            Database.Similars.Delete(similars);
            Database.Save();
        }

        public List<Similar> GetListSimilar(string name)
        {
            List<Similar> listSimilar = new List<Similar>();
            dynamic ResultJson = GetResponse("http://ws.audioscrobbler.com/2.0/?method=artist.getsimilar&artist=", name, 1, 12);
            string nameSimilar = "";
            string photoSimilar = "";
            foreach (var artist in ResultJson.similarartists.artist)
            {
                nameSimilar = artist.name;
                foreach (dynamic dyn in artist.image)
                {
                    if (dyn.size == "mega")
                    {
                        photoSimilar = dyn.text;
                        break;
                    }
                }
                Similar similar = new Similar
                {
                    SimilarId = Guid.NewGuid(),
                    Name = nameSimilar,
                    Photo = photoSimilar
                };
                listSimilar.Add(similar);
            }
            return listSimilar;
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

    }
}
