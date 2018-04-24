using System;
using System.Collections.Generic;
using System.Text;
using OAA.Data;
using OAA.Repo.Intarfaces;
using OAA.Service.Interfaces;

namespace OAA.Service.Service
{
    public class SimilarService : ISimilarService
    {
        IUnitOfWork Database { get; set; }

        public SimilarService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Similar GetSimilar(Guid id)
        {
            return Database.Similars.Get(id);
        }
    }
}
