using ModMadnessDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Interface
{
    public interface IGameVersionService
    {
        GameVersion Create(GameVersion gameVersion);
        GameVersion Update(GameVersion gameVersion);
        GameVersion Delete(Guid id);
        GameVersion? GetById(Guid id);
        List<GameVersion> GetAll();
    }
}
