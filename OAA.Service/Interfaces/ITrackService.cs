using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface ITrackService
    {
        IEnumerable<Track> GetAll();
        Track GetFromBd(Guid id);
        void Create(Track track);
        void Update(Track track);
        void Delete(Track track);
        List<Track> GetTopTracks(string name, int count = 24, int page = 1);
    }
}
