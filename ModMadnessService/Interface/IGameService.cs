using ModMadnessDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Interface
{
    public interface IGameService
    {
        Game Create(Game game);
        Game Update(Game game);
        Game Delete(Guid id);
        Game? GetById(Guid id);
        List<Game> GetAll();
        
    }
}
