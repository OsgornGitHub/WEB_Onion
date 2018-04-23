using System;
using System.Collections.Generic;
using System.Text;
using WEB.Data;

namespace WEB.Service.Interfaces
{
    public interface ISimilarService
    {
        Similar GetSimilar(Guid id);
    }
}
