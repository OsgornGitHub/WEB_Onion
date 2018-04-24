using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface ISimilarService
    {
        Similar GetSimilar(Guid id);
    }
}
