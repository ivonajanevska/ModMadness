using ModMadnessDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Interface
{
    public interface IPlatformService
    {
        Platform Create(Platform platform);
        Platform Update(Platform platform);
        Platform Delete(Guid id);
        Platform? GetById(Guid id);
        List<Platform> GetAll();
    }
}
