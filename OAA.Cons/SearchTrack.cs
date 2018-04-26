using OAA.Data;
using OAA.Service.Service;
using System.IO;
using System.Linq;


namespace OAA.Cons
{
    public class SearchTrack
    {
        private readonly TrackService trackService;
        private readonly AlbumService albumService;

        public SearchTrack(AlbumService albumService, TrackService trackService)
        {
            this.trackService = trackService;
            this.albumService = albumService;
        }

        public void Search()
        {
            string[] filenames = Directory.GetFiles("D:\\WEB_Onion\\Tracks", "*.mp3", SearchOption.AllDirectories);
            foreach(var link in filenames)
            {
                // nameTrack_nameArtist.mp3
                var nameTrack = "";
                var nameArtist = "";
                var splited = link.Split("+");
                nameTrack = splited[0].Split("\\")[3];
                nameArtist = splited[1].Replace(".mp3", "");
                if (albumService.GetAll().FirstOrDefault(a => a.NameArtist == nameArtist).Tracks.FirstOrDefault(t => t.Name == nameTrack) != null)
                {
                    AddLinkToDb(nameTrack, nameArtist, link);
                }
                else trackService.AddTrackFromLast(nameTrack, nameArtist, link);
            } 
        }

        public void AddLinkToDb(string nameTrack, string nameArtist, string link)
        {
            string nameT = nameTrack;
            string nameA = nameArtist;
            Track track = albumService.GetAll().FirstOrDefault(a => a.NameArtist == nameArtist).Tracks.FirstOrDefault(t => t.Name == nameTrack);
            track.Link = link;
            trackService.Update(track);
        }
    }
}
