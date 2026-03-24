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
    public class GameService : IGameService
    {
        public readonly IRepository<Game> _gameRepository;

        public GameService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Game Create(Game game)
        {
            return _gameRepository.Create(game);
        }

        public Game Delete(Guid id)
        {
            var game = GetById(id);

            if (game == null)
            {
                throw new Exception("Game not found");
            }
            return _gameRepository.Delete(game);
        }

        public List<Game> GetAll()
        {
            return _gameRepository.GetAll(selector: game => game).ToList();
        }

        public Game? GetById(Guid id)
        {
            return _gameRepository.Get(selector: game => game, filter: game => game.Id.Equals(id));
        }
        

        public Game Update(Game game)
        {
            return _gameRepository.Update(game);
        }
    }
}
