using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;
using OAA.Service.Interfaces;

namespace OAA.Service.Service
{
    public class TrackService : ITrackService
    {
        IUnitOfWork Database { get; set; }

        public TrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Track GetTrack(Guid id)
        {
            return Database.Tracks.Get(id);
        }
    }
}
