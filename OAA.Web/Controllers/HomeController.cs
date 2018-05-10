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
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly ITrackService _trackService;
        private readonly ISimilarService _similarService;
        private readonly IHostingEnvironment _appEnvironment;



        public HomeController(IArtistService artistService, IAlbumService albumService, ITrackService trackService, ISimilarService similarService, IHostingEnvironment appEnvironment)
        {
            this._artistService = artistService;
            this._albumService = albumService;
            this._trackService = trackService;
            this._similarService = similarService;
            this._appEnvironment = appEnvironment;
        }

        public IActionResult Index(int page = 1, int count = 24)
        {
            List<Artist> list = new List<Artist>();
            list = _artistService.GetNextPage(page, count);
            return View(list);
        }


        public PartialViewResult GetPage(int page = 1, int count = 24)
        {
            List<Artist> list = new List<Artist>();
            list = _artistService.GetNextPage(page, count);
            return PartialView(list);
        }



        public JsonResult GetTopArtistJson(int page, int count)
        {
            var list = _artistService.GetNextPage(page, count);
            return Json(list);
        }

        [HttpGet]
        public IActionResult GetArtist(string name)
        {
            if (_artistService.GetAll().Where(a => a.Name == name).Count() != 0)
            {
                return View(_artistService.GetAll().FirstOrDefault(a => a.Name == name));
            }
            Artist artist = _artistService.GetArtist(name);
            _artistService.Create(artist);
            return View(artist);
        }


        [HttpPost]
        public PartialViewResult GetListSimilar(string name)
        {
            var nameForRequest = name.Replace(" ", "+");
            List<Similar> listSimilar = new List<Similar>();
            List<SimilarViewModel> listModel = new List<SimilarViewModel>();
            var listSimInDb = _similarService.GetAll().Where(a => a.ArtistId == _artistService.GetAll().FirstOrDefault(b => b.Name == name).Id);
            if (listSimInDb.Count() != 0)
            {
                foreach (Similar s in listSimInDb)
                {
                    var modelSim = new SimilarViewModel()
                    {
                        Name = s.Name,
                        Photo = s.Photo
                    };
                    listModel.Add(modelSim);
                }
                return PartialView(listModel);
            }
            listSimilar = _similarService.GetListSimilar(nameForRequest);
            foreach (Similar sim in listSimilar)
            {
                sim.ArtistId = _artistService.Get(name).Id;
                _similarService.Create(sim);
                var model = new SimilarViewModel()
                {
                    Name = sim.Name,
                    Photo = sim.Photo
                };
                listModel.Add(model);
            }
            return PartialView(listModel);
        }

        [HttpPost]
        public PartialViewResult GetTopAlbum(string name, int page, int count)
        {
            List<Album> topAlbums = new List<Album>();
            List<AlbumViewModel> listModel = new List<AlbumViewModel>();
            var listAlbInDb = _albumService.GetAlbumsByNameArtist(name);
            if (listAlbInDb.Count() != 0)
            {
                foreach (Album a in listAlbInDb)
                {
                    var modelAlb = new AlbumViewModel()
                    {
                        NameAlbum = a.Name,
                        NameArtist = a.NameArtist,
                        Cover = a.Cover
                    };
                    listModel.Add(modelAlb);

                }
            }

            var nameForRequest = name.Replace(" ", "+");
            topAlbums = _albumService.GetTopAlbum(nameForRequest, page, count);
            foreach (var alb in topAlbums)
            {
                var albumInDb = _albumService.Get(alb.Name);
                if (albumInDb == null)
                {
                    alb.ArtistId = _artistService.Get(name).Id;
                    _albumService.Create(alb);
                    var model = new AlbumViewModel()
                    {
                        NameAlbum = alb.Name,
                        Cover = alb.Cover,
                        NameArtist = alb.NameArtist
                    };
                    listModel.Add(model);
                }

            }
            return PartialView(listModel);
        }

        [HttpPost]
        public PartialViewResult GetTopTracks(string name, int count = 24, int page = 1)
        {
            var nameForRequest = name.Replace(" ", "+");
            List<Track> tracks = _trackService.GetTopTracks(nameForRequest, count, page);
            List<TrackViewModel> listTrack = new List<TrackViewModel>();
            foreach(var tr in tracks)
            {
                var model = new TrackViewModel()
                {
                    Name = tr.Name
                };
                listTrack.Add(model);
            }
            return PartialView(listTrack);
        }


        public IActionResult GetAlbum(string nameArtist, string nameAlbum)
        {
            var nameArtistForRequest = nameArtist.Replace(" ", "+");
            var nameAlbumForRequest = nameAlbum.Replace(" ", "+");

            var listTrackInDb = _trackService.GetAll().Where(a => a.AlbumId == _albumService.Get(nameAlbum).Id);
            if (listTrackInDb.Count() == 0)
            {
                Album alb = _albumService.GetAll().Where(a => a.Name == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist);

                Album album = _albumService.GetAlbum(nameArtistForRequest, nameAlbumForRequest);
                var artistId = _artistService.Get(nameArtist).Id;
                foreach (Track track in album.Tracks)
                {
                    track.AlbumId = alb.Id;
                    track.NameAlbum = nameAlbum;
                    _trackService.Create(track);
                }
                _albumService.Update(alb);
                return View(album);
            }
            else
            {
                return View(_albumService.GetAll().Where(a => a.Name == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist));
            }

        }

        public IActionResult GetCountPageTopArtist(int page, int count)
        {
            return Ok(_artistService.GetCountPageTopArtist(page, count));
        }

        [HttpPost]
        public IActionResult AddFile(AddTrackViewModel model)
        {
            if (model.File != null)
            {
                string path = model.File.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    model.File.CopyToAsync(fileStream);
                }
                Album album = _albumService.GetById(model.AlbumId);
                Track track = new Track()
                {
                    Id = model.Id,
                    Name = model.Name,
                    AlbumId = model.AlbumId,
                    Link = "http://localhost:52527/tracks/" + path,
                    NameAlbum = album.Name
                };
                _trackService.Delete(track);
                _trackService.Create(track);
                return RedirectToAction("GetAlbum", "Home", new { nameArtist = album.NameArtist, nameAlbum = album.Name });
            }
            return StatusCode(400);

        }

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
