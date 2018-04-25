using OAA.Data;
using OAA.Service.Interfaces;
using System.IO;
using System.Linq;

namespace OAA.Cons
{
    public class SearchTrack
    {
        private readonly ITrackService trackService;
        private readonly IAlbumService albumService;

        public SearchTrack(IAlbumService albumService, ITrackService trackService)
        {
            this.trackService = trackService;
            this.albumService = albumService;
        }

        public string Search(string nameTrack, string nameArtist)
        {
            var nameT = IsValidName(nameTrack);
            var nameA = IsValidName(nameArtist);
            string searchParameter = nameT + "_" + nameA + ".mp3";
            string[] filenames = Directory.GetFiles("D:\\WEB_Onion\\Tracks");
            foreach (string name in filenames)
            {
                if (name == searchParameter)
                {
                    return name;
                }
            }
            return "";
        }

        public void Result(string link)
        {
            var nameTrack = "";
            var nameArtist = "";
            var splited = link.Split("_");
            nameTrack = splited[0];
            nameArtist = splited[1];
            if (Search(nameTrack, nameArtist) != "" || albumService.GetAll().FirstOrDefault(a => a.NameArtist == nameArtist).Tracks.FirstOrDefault(t => t.Name == nameTrack) != null)
            {
                AddLinkToDb(nameTrack, nameArtist, Search(nameTrack, nameArtist));
            }
            else trackService.AddTrackFromLast(nameTrack, nameArtist, link);
        }



        public void AddLinkToDb(string nameTrack, string nameArtist, string link)
        {
            Track track = albumService.GetAll().FirstOrDefault(a => a.NameArtist == nameArtist).Tracks.FirstOrDefault(t => t.Name == nameTrack);
            track.Link = link;
            trackService.Update(track);
        }

        private string IsValidName(string name)
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
    }
}
