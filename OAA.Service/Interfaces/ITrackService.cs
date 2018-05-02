using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface ITrackService
    {
        IEnumerable<Track> GetAll();
        void Create(Track track);
        void Update(Track track);
        void Delete(Track track);
        Track Get(string name);
        Track AddTrackFromLast(string nameTrack, string nameArtist, string link);
        List<Track> GetTopTracks(string name, int count = 24, int page = 1);
        string GetAlbumTrackName(string nameArtist, string nameTrack);
    }
}
