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

        public IActionResult Index()
        {
            //for (int i = 0; i < 2; i++)
            //{
            //    Artist model = new Artist()
            //    {
            //        Name = "qwe",
            //        Biography = "edsf",
            //        Photo = "wqewe"
            //    };
            //artistService.Create(model);
            //}
            IEnumerable<Artist> artists = artistService.GetAll();

            return View(artists);
        }

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
