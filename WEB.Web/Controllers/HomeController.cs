using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Service.Interfaces;
using WEB.Web.Models;

namespace WEB.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IArtistService artistService;
        private readonly IAlbumService albumService;
        private readonly ISimilarService similarService;
        private readonly ITrackService trackService;

        public HomeController(IArtistService artistService, IAlbumService albumService, ITrackService trackService, ISimilarService similarService)
        {
            this.artistService = artistService;
            this.albumService = albumService;
            this.trackService = trackService;
            this.similarService = similarService;
        }


        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
