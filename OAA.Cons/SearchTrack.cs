using OAA.Data;
using OAA.Service.Interfaces;
using System.Collections.Generic;
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

        public void Search()
        {
            string[] filenames = Directory.GetFiles("D:\\WEB_Onion\\Tracks", ".mp3", SearchOption.AllDirectories);
            foreach(var track in filenames)
            {
                // nameTrack_nameArtist.mp3
                var nameTrack = "";
                var nameArtist = "";
                var splited = track.Split("_");
                nameTrack = splited[0];
                nameArtist = splited[1].Replace(".mp3", "");
                if (albumService.GetAll().FirstOrDefault(a => a.NameArtist == nameArtist).Tracks.FirstOrDefault(t => t.Name == nameTrack) != null)
                {
                    AddLinkToDb(nameTrack, nameArtist, track);
                }
                else trackService.AddTrackFromLast(nameTrack, nameArtist, track);
            } 
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
