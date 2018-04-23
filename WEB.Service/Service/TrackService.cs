using System;
using System.Collections.Generic;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;
using WEB.Service.Interfaces;

namespace WEB.Service.Service
{
    public class TrackService : ITrackService
    {
        private IRepository<Track> trackRepository;

        public TrackService(IRepository<Track> trackRepository)
        {
            this.trackRepository = trackRepository;
        }

        public Track GetTrack(Guid id)
        {
            return trackRepository.Get(id);
        }
    }
}
