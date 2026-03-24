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
    public class ModService : IModService
    {
        public readonly IRepository<Mod> _modRepository;

        public ModService(IRepository<Mod> modRepository)
        {
            _modRepository = modRepository;
        }

        public Mod Create(Mod mod)
        {
            return _modRepository.Create(mod);
        }

        public Mod Delete(Guid id)
        {
            var mod = GetById(id);

            if (mod == null)
            {
                throw new Exception("Mod not found");
            }
            return _modRepository.Delete(mod);
        }

        public List<Mod> GetAll()
        {
            return _modRepository.GetAll(selector: mod => mod).ToList();
        }

        public Mod? GetById(Guid id)
        {
            return _modRepository.Get(selector: mod => mod, filter: mod => mod.Id.Equals(id));
        }

        public Mod Update(Mod mod)
        {
            return _modRepository.Update(mod);
        }
    }
}
