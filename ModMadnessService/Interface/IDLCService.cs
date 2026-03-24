using ModMadnessDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Interface
{
    public interface IDLCService
    {
        DLC Create(DLC dlc);
        DLC Update(DLC dlc);
        DLC Delete(Guid id);
        DLC? GetById(Guid id);
        List<DLC> GetAll();
    }
}
