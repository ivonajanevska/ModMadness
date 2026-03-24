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
    public class GameVersionService : IGameVersionService
    {
        public readonly IRepository<GameVersion> _gameVersionRepository;

        public GameVersionService(IRepository<GameVersion> gameVersionRepository)
        {
            _gameVersionRepository = gameVersionRepository;
        }

        public GameVersion Create(GameVersion gameVersion)
        {
            return _gameVersionRepository.Create(gameVersion);
        }

        public GameVersion Delete(Guid id)
        {
            var gameVersion = GetById(id);

            if (gameVersion == null)
            {
                throw new Exception("Game not found");
            }
            return _gameVersionRepository.Delete(gameVersion);
        }

        public List<GameVersion> GetAll()
        {
            return _gameVersionRepository.GetAll(selector: game => game).ToList();
        }

        public GameVersion? GetById(Guid id)
        {
            return _gameVersionRepository.Get(selector: game => game, filter: game => game.Id.Equals(id));
        }

        public GameVersion Update(GameVersion gameVersion)
        {
            return _gameVersionRepository.Update(gameVersion);
        }
    }
}
