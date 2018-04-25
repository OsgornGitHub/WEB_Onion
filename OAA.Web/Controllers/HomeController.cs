using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OAA.Data;
using OAA.Service.Interfaces;
using OAA.Web.Models;

namespace OAA.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IAlbumService albumService;
        private readonly ITrackService trackService;
        private readonly ISimilarService similarService;

        public HomeController(IArtistService artistService, IAlbumService albumService, ITrackService trackService, ISimilarService similarService)
        {
            this.artistService = artistService;
            this.albumService = albumService;
            this.trackService = trackService;
            this.similarService = similarService;
        }

        public IActionResult Index(int page = 1)
        {
            List<Artist> list = new List<Artist>();
            var pageNum = page;
            var count = 24;
            list = artistService.GetNextPage(pageNum, count);
            return View(list);
        }


        public JsonResult GetTopArtistJson(int page, int count)
        {
            var list = artistService.GetNextPage(page, count);
            return Json(list);
        }

        [HttpGet]
        public IActionResult GetArtist(string name)
        {    
            if(artistService.GetAll().FirstOrDefault(a => a.Name == name) != null)
            {
                return View(artistService.GetAll().FirstOrDefault(a => a.Name == name));
            }
            Artist artist = artistService.GetArtist(name);
            artistService.Create(artist);
            return View(artist);
        }


        [HttpGet]
        public IActionResult GetListSimilar(string name)
        {
            var nameForRequest = IsValidName(name);
            if (artistService.GetAll().FirstOrDefault(a => a.Name == name).Similars != null)
            {
                return Ok(artistService.GetAll().FirstOrDefault(a => a.Name == name).Similars);
            }
            List<Similar> listSimilar = new List<Similar>();
            List<SimilarViewModel> listModel = new List<SimilarViewModel>();
            listSimilar = similarService.GetListSimilar(nameForRequest);
            foreach(Similar sim in listSimilar)
            {
                sim.ArtistId = artistService.GetAll().FirstOrDefault(a => a.Name == name).ArtistId;
                similarService.Create(sim);
                var model = new SimilarViewModel()
                {
                    Name = sim.Name,
                    Photo = sim.Photo
                };
                listModel.Add(model);
            }
            return Ok(listModel);
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

        [HttpGet]
        public IActionResult GetTopAlbum(string name, int page, int count)
        {
            List<Album> topAlbums = new List<Album>();
            List<AlbumViewModel> listModel = new List<AlbumViewModel>();
            if (artistService.GetAll().FirstOrDefault(a => a.Name == name).Albums != null)
            {
                return Ok(artistService.GetAll().FirstOrDefault(a => a.Name == name).Albums);
            }
            var nameForRequest = IsValidName(name);
            topAlbums = albumService.GetTopAlbum(nameForRequest, page, count);
            foreach (var alb in topAlbums)
            {
                alb.ArtistId = artistService.GetAll().FirstOrDefault(a => a.Name == name).ArtistId;
                albumService.Create(alb);
                var model = new AlbumViewModel()
                {
                    NameAlbum = alb.NameAlbum,
                    Cover = alb.Cover,
                    NameArtist = alb.NameArtist
                };
                listModel.Add(model);
            }
            return Ok(listModel);
        }

        public List<Track> GetTopTracks(string name, int count = 24, int page = 1)
        {
            var nameForRequest = IsValidName(name);
            return trackService.GetTopTracks(nameForRequest, count, page);
        }


        public IActionResult GetAlbum(string nameArtist, string nameAlbum)
        {

            if(albumService.GetAll().FirstOrDefault(a => a.NameAlbum == nameAlbum) != null)
            {
                return View(albumService.GetAll().FirstOrDefault(a => a.NameAlbum == nameAlbum));
            }
            var nameArtistForRequest = IsValidName(nameArtist);
            var nameAlbumForRequest = IsValidName(nameAlbum);
            Album album = albumService.GetAlbum(nameArtistForRequest, nameAlbumForRequest);
            var artistId = artistService.GetAll().FirstOrDefault(a => a.Name == nameArtist).ArtistId;
            foreach(Track track in album.Tracks)
            {
                track.AlbumId = albumService.GetAll().FirstOrDefault(a => a.NameAlbum == nameAlbum).AlbumId;
                trackService.Create(track);
            }
            album.ArtistId = artistId;
            albumService.Create(album);

            return View(album);
        }
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
