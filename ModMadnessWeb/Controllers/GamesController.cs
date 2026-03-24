using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModMadnessDomain.Domain;
using ModMadnessRepository.Data;
using ModMadnessService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModMadnessWeb.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IRawgApiService _rawgApiService; // Додадено

        public GamesController(IGameService gameService, IRawgApiService rawgApiService)
        {
            _gameService = gameService;
            _rawgApiService = rawgApiService;
        }


        // GET: Games
        public IActionResult Index()
        {
            return View(_gameService.GetAll());
        }

        // GET: Games/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetById(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Type,Author,Image,Id")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.Id = Guid.NewGuid();
                _gameService.Create(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }
        [HttpPost]
        public async Task<IActionResult> Import(string searchTitle)
        {
            if (string.IsNullOrEmpty(searchTitle))
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Го користиме веќе дефинираниот сервис
                // Забелешка: Ќе го прошириме GetGamesAsync подоцна за да прифаќа searchTitle
                var games = await _rawgApiService.SearchGamesAsync(searchTitle);

                if (games != null && games.Any())
                {
                    var rawgGame = games.First();

                    var newGame = new Game
                    {
                        Id = Guid.NewGuid(),
                        Title = rawgGame.Name,
                        Description = $"Imported from RAWG. Release date: {rawgGame.Released}",

                        // Проверка: ако нема слика од API-то, стави placeholder
                        Image = string.IsNullOrEmpty(rawgGame.Background_Image)
                                ? "https://via.placeholder.com/400x225?text=No+Image"
                                : rawgGame.Background_Image,

                        Author = "RAWG Database",
                        Type = "Digital"
                    };

                    _gameService.Create(newGame);
                }
            }
            catch (Exception ex)
            {
                // Овде можеш да додадеш порака за грешка
                TempData["Error"] = "Could not import game: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Games/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetById(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Description,Type,Author,Image,Id")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _gameService.Update(game);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetById(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _gameService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(Guid id)
        {
            return _gameService.GetById(id) != null;
        }
    }
}
