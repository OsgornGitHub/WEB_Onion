using OAA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class Album : BaseEntity
    {
        public string Cover { get; set; }
        public string NameArtist { get; set; }

        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
