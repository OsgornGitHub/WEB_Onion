using OAA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class Artist : BaseEntity
    {
        public string Photo { get; set; }
        public string Biography { get; set; }

        public ICollection<Album> Albums { get; set; }
        public ICollection<Similar> Similars { get; set; }

        public Artist()
        {
            Albums = new List<Album>();
            Similars = new List<Similar>();
        }
    }
}
