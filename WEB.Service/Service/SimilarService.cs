using System;
using System.Collections.Generic;
using System.Text;
using WEB.Data;
using WEB.Repo.Intarfaces;
using WEB.Service.Interfaces;

namespace WEB.Service.Service
{
    public class SimilarService : ISimilarService
    {
        private IRepository<Similar> similarRepository;

        public SimilarService(IRepository<Similar> similarRepository)
        {
            this.similarRepository = similarRepository;
        }

        public Similar GetSimilar(Guid id)
        {
            return similarRepository.Get(id);
        }
    }
}
