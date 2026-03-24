using ModMadnessDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Interface
{
    public interface IModService
    {
        Mod Create(Mod mod);
        Mod Update(Mod mod);
        Mod Delete(Guid id);
        Mod? GetById(Guid id);
        List<Mod> GetAll();
    }
}
