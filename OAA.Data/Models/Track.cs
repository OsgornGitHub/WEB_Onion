using OAA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class Track : BaseEntity
    {
        public string NameAlbum { get; set; }
        public string Link { get; set; }
        public string Cover { get; set; }

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
