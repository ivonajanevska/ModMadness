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
    public class DLCService : IDLCService
    {
        public readonly IRepository<DLC> _dlcRepository;

        public DLCService(IRepository<DLC> dlcRepository)
        {
            _dlcRepository = dlcRepository;
        }

        public DLC Create(DLC dlc)
        {
            return _dlcRepository.Create(dlc);
        }

        public DLC Delete(Guid id)
        {
            var dlc = GetById(id);

            if (dlc == null)
            {
                throw new Exception("DLC not found");
            }
            return _dlcRepository.Delete(dlc);
        }

        public List<DLC> GetAll()
        {
            return _dlcRepository.GetAll(selector: dlc => dlc).ToList();
        }

        public DLC? GetById(Guid id)
        {
            return _dlcRepository.Get(selector: dlc => dlc, filter: dlc => dlc.Id.Equals(id));
        }

        public DLC Update(DLC dlc)
        {
            return _dlcRepository.Update(dlc);
        }
    }
}
