using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Data
{
    public class Similar
    {
        public Guid SimilarId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }

        public Guid? ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

    }
}
