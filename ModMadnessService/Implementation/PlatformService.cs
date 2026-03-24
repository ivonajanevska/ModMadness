using Microsoft.VisualBasic;
using ModMadnessDomain.Domain;
using ModMadnessRepository.Interface;
using ModMadnessService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Implementation
{
    public class PlatformService : IPlatformService
    {
        private readonly IRepository<Platform> _platformRepository;

        public PlatformService(IRepository<Platform> platformRepository)
        {
            _platformRepository = platformRepository;
        }

        public Platform Create(Platform platform)
        {
            return _platformRepository.Create(platform);
        }

        public Platform Delete(Guid id)
        {
            var platform = GetById(id);

            if (platform == null)
            {
                throw new Exception("Platform not found");
            }
            return _platformRepository.Delete(platform);
        }

        public List<Platform> GetAll()
        {
            return _platformRepository.GetAll(selector: platform => platform).ToList();
        }

        public Platform? GetById(Guid id)
        {
            return _platformRepository.Get(selector: platform => platform, filter: platform => platform.Id.Equals(id));
        }

        public Platform Update(Platform platform)
        {
            return _platformRepository.Update(platform);
        }
    }
}
