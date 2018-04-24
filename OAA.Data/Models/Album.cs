﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class Album
    {
        public Guid AlbumId { get; set; }
        public string Cover { get; set; }
        public string NameAlbum { get; set; }
        public string NameArtist { get; set; }

        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public List<Track> Tracks { get; set; }
    }
}