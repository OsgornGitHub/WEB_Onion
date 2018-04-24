using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface ITrackService
    {
        Track GetTrack(Guid id);
    }
}
