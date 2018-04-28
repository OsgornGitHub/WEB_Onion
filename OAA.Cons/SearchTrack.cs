using OAA.Data;
using OAA.Service.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace OAA.Cons
{
    public class SearchTrack
    {
        private readonly TrackService trackService;
        private readonly AlbumService albumService;
        private readonly ArtistService artistService;

        public SearchTrack(AlbumService albumService, TrackService trackService, ArtistService artistService)
        {
            this.trackService = trackService;
            this.albumService = albumService;
            this.artistService = artistService;
        }

        public void Search()
        {
            string[] filenames = Directory.GetFiles("D:\\WEB_Onion\\Tracks", "*.mp3", SearchOption.AllDirectories);
            foreach (var link in filenames)
            {
                // nameTrack-nameArtist.mp3
                var nameTrack = "";
                var nameArtist = "";
                var splited = link.Split("-");
                nameTrack = splited[0].Split("\\")[3];
                nameArtist = splited[1].Replace(".mp3", "");



                var nameT = nameTrack.Replace(" ", "+");
                var nameA = nameArtist.Replace(" ", "+");

                if (artistService.GetAll().Where(a => a.Name == nameArtist).Count() == 0)
                {
                    AddArtistToDb(nameArtist);
                }

                var nameAlbum = trackService.GetAlbumTrackName(nameA, nameT);
                Album album = albumService.GetAll().Where(a => a.NameArtist == nameArtist).FirstOrDefault(b => b.NameAlbum == nameAlbum);

                if (album == null)
                {
                    Album alb = albumService.GetAlbum(nameArtist, nameAlbum);
                    alb.ArtistId = artistService.GetAll().FirstOrDefault(a => a.Name == nameArtist).ArtistId;
                    albumService.Create(alb);
                }

                if (albumService.GetAll().FirstOrDefault(a => a.NameArtist == nameArtist).Tracks.Where(t => t.Name == nameTrack).Count() != 0)
                {
                    AddLinkToDb(nameTrack, nameArtist, link.Replace(" ", "+"));
                }
                else
                {
                    Track track = trackService.AddTrackFromLast(nameTrack, nameArtist, link.Replace(" ", "+"));
                    track.AlbumId = albumService.GetAll().Where(a => a.NameAlbum == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist).AlbumId;
                    track.NameAlbum = nameAlbum;
                    trackService.Create(track);
                }
            }
        }

        public void AddArtistToDb(string name)
        {
            Artist artist = artistService.GetArtist(name);
            artistService.Create(artist);
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
