using System;
using System.Collections.Generic;
using System.Text;
using WEB.Data;

namespace WEB.Service.Interfaces
{
    public interface IAlbumService
    {
        Album GetAlbum(Guid id);
    }
}
