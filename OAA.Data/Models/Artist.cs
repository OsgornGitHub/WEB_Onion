using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class Artist
    {
        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Biography { get; set; }

        public ICollection<Album> Albums { get; set; }
        public ICollection<Similar> Similars { get; set; }
    }
}
