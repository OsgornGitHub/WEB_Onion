using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAA.Web.Models
{
    public class ArtistViewModel
    {
        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
