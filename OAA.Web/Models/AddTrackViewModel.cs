using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAA.Web.Models
{
    public class AddTrackViewModel
    {
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
