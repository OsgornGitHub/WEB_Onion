using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public IActionResult GetJson(int page, int count)
        {
            var list = artistService.GetNextPage(page, count);
            return Json(list);
        }

        [HttpGet]
        public IActionResult GetArtist(string name)
        {
            Artist artist = artistService.GetArtist(name);         
            artistService.Create(artist);
            return View(artist);
        }

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
