﻿using System.Collections.Generic;
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
            if (artistService.GetAll().FirstOrDefault(a => a.Name == name) != null)
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
            var ga = albumService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == name).ArtistId).Count();
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
                return Ok(listModel);
            }
            var nameForRequest = name.Replace(" ", "+");
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
            var nameForRequest = name.Replace(" ", "+");
            return trackService.GetTopTracks(nameForRequest, count, page);
        }


        public IActionResult GetAlbum(string nameArtist, string nameAlbum)
        {
            if (albumService.GetAll().Where(a => a.ArtistId == artistService.GetAll().FirstOrDefault(b => b.Name == nameArtist).ArtistId).Count() != 0)
            {

                if (albumService.GetAll().Where(a => a.NameAlbum == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist).Tracks == null)
                {
                    Album alb = albumService.GetAll().Where(a => a.NameAlbum == nameAlbum).FirstOrDefault(b => b.NameArtist == nameArtist);
                    var nameArtistForRequest = nameArtist.Replace(" ", "+");
                    var nameAlbumForRequest = nameAlbum.Replace(" ", "+");
                    Album album = albumService.GetAlbum(nameArtistForRequest, nameAlbumForRequest);
                    var artistId = artistService.GetAll().FirstOrDefault(a => a.Name == nameArtist).ArtistId;
                    foreach (Track track in album.Tracks)
                    {
                        track.AlbumId = alb.AlbumId;
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
            else
            {
                var nameArtistForRequest = nameArtist.Replace(" ", "+");
                var nameAlbumForRequest = nameAlbum.Replace(" ", "+");
                Album album = albumService.GetAlbum(nameArtistForRequest, nameAlbumForRequest);
                var artistId = artistService.GetAll().FirstOrDefault(a => a.Name == nameArtist).ArtistId;
                foreach (Track track in album.Tracks)
                {
                    track.AlbumId = album.AlbumId;
                    trackService.Create(track);
                }
                albumService.Create(album);
                return View(album);
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
