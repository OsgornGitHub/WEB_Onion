using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _appEnvironment;



        public HomeController(IArtistService artistService, IAlbumService albumService, ITrackService trackService, ISimilarService similarService, IHostingEnvironment appEnvironment)
        {
            this.artistService = artistService;
            this.albumService = albumService;
            this.trackService = trackService;
            this.similarService = similarService;
            this._appEnvironment = appEnvironment;
        }

        public IActionResult Index(int page = 1)
        {
            List<Artist> list = new List<Artist>();
            var pageNum = page;
            var count = 24;
            list = artistService.GetNextPage(pageNum, count);
            return View(list);
        }

        public IActionResult DownloadTrack(string link)
        {
            string good_link = link.Replace("+", " ");
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, good_link);
            // Тип файла - content-type
            string file_type = "music/mp3";
            // Имя файла - необязательно
            var file_name = good_link.Split("\\")[3];
            return PhysicalFile(file_path, file_type, file_name);
        }


        public JsonResult GetTopArtistJson(int page, int count)
        {
            var list = artistService.GetNextPage(page, count);
            return Json(list);
        }

        [HttpGet]
        public IActionResult GetArtist(string name)
        {
            if (artistService.GetAll().Where(a => a.Name == name).Count() != 0)
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
            var nameForRequest = name.Replace(" ", "+");
            List<Similar> listSimilar = new List<Similar>();
            List<SimilarViewModel> listModel = new List<SimilarViewModel>();
            if (similarService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId).Count() != 0)
            {
                foreach (Similar s in similarService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId))
                {
                    var modelSim = new SimilarViewModel()
                    {
                        Name = s.Name,
                        Photo = s.Photo
                    };
                    listModel.Add(modelSim);
                }
                return Ok(listModel);
            }
            listSimilar = similarService.GetListSimilar(nameForRequest);
            foreach (Similar sim in listSimilar)
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

        [HttpGet]
        public IActionResult GetTopAlbum(string name, int page, int count)
        {
            List<Album> topAlbums = new List<Album>();
            List<AlbumViewModel> listModel = new List<AlbumViewModel>();

            if (albumService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId).Count() != 0)
            {
                foreach (Album a in albumService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId))
                {
                    var modelAlb = new AlbumViewModel()
                    {
                        NameAlbum = a.NameAlbum,
                        NameArtist = a.NameArtist,
                        Cover = a.Cover
                    };
                    listModel.Add(modelAlb);
                }
            }

            var nameForRequest = name.Replace(" ", "+");
            topAlbums = albumService.GetTopAlbum(nameForRequest, page, count);
            foreach (var alb in topAlbums)
            {

                if (albumService.GetAll().FirstOrDefault(b => b.NameAlbum == alb.NameAlbum) == null)
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

            }
            return Ok(listModel);





            //if (albumService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId).Count() != 0)
            //{
            //    foreach (Album a in albumService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId))
            //    {
            //        var modelAlb = new AlbumViewModel()
            //        {
            //            NameAlbum = a.NameAlbum,
            //            NameArtist = a.NameArtist,
            //            Cover = a.Cover
            //        };
            //        listModel.Add(modelAlb);
            //    }
            //    return Ok(listModel);
            //}
            //var nameForRequest = name.Replace(" ", "+");
            //topAlbums = albumService.GetTopAlbum(nameForRequest, page, count);
            //foreach (var alb in topAlbums)
            //{
            //    alb.ArtistId = artistService.GetAll().FirstOrDefault(a => a.Name == name).ArtistId;
            //    albumService.Create(alb);
            //    var model = new AlbumViewModel()
            //    {
            //        NameAlbum = alb.NameAlbum,
            //        Cover = alb.Cover,
            //        NameArtist = alb.NameArtist
            //    };
            //    listModel.Add(model);
            //}
            //return Ok(listModel);
        }

        public List<Track> GetTopTracks(string name, int count = 24, int page = 1)
        {
            var nameForRequest = name.Replace(" ", "+");
            return trackService.GetTopTracks(nameForRequest, count, page);
        }


        public IActionResult GetAlbum(string nameArtist, string nameAlbum)
        {
            var nameArtistForRequest = nameArtist.Replace(" ", "+");
            var nameAlbumForRequest = nameAlbum.Replace(" ", "+");

            var d = trackService.GetAll().Where(a => a.AlbumId == albumService.GetAll().FirstOrDefault(b => b.NameAlbum == nameAlbum).AlbumId);
            if (trackService.GetAll().Where(a => a.AlbumId == albumService.GetAll().FirstOrDefault(b => b.NameAlbum == nameAlbum).AlbumId).Count() == 0)
            {
                Album alb = albumService.GetAll().Where(a => a.NameAlbum == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist);

                Album album = albumService.GetAlbum(nameArtistForRequest, nameAlbumForRequest);
                var artistId = artistService.GetAll().FirstOrDefault(a => a.Name == nameArtist).ArtistId;
                foreach (Track track in album.Tracks)
                {
                    track.AlbumId = alb.AlbumId;
                    track.NameAlbum = nameAlbum;
                    trackService.Create(track);
                }
                albumService.Update(alb);
                return View(album);
            }
            else
            {
                return View(albumService.GetAll().Where(a => a.NameAlbum == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist));
            }

        }

        public IActionResult GetCountPageTopArtist(int page, int count)
        {
            return Ok(artistService.GetCountPageTopArtist(page, count));
        }
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
