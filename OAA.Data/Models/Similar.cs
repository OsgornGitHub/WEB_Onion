using OAA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class Similar : BaseEntity
    {
        public string Photo { get; set; }

        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
