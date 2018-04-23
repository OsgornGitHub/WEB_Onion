using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Data
{
    public class Track
    {
        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Cover { get; set; }

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
