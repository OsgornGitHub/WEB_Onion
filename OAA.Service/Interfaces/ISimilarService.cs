using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;

namespace OAA.Service.Interfaces
{
    public interface ISimilarService
    {
        IEnumerable<Similar> GetAll();
        void Create(Similar similar);
        void Update(Similar similar);
        void Delete(Similar similar);
        List<Similar> GetListSimilar(string name);
    }
}
