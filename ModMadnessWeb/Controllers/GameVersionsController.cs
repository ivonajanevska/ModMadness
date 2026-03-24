using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModMadnessDomain.Domain;
using ModMadnessRepository.Data;
using ModMadnessService.Interface;

namespace ModMadnessWeb.Controllers
{
    public class GameVersionsController : Controller
    {
        private readonly IGameVersionService _gameVersionService;
        private readonly IGameService _gameService;

        public GameVersionsController(IGameVersionService gameVersionService, IGameService gameService)
        {
            _gameVersionService = gameVersionService;
            _gameService = gameService;
        }

        // ПОПРАВЕНО: Прифаќа gameId и е единствен Index метод
        public IActionResult Index(Guid? gameId)
        {
            var allVersions = _gameVersionService.GetAll();

            if (gameId.HasValue)
            {
                var filtered = allVersions.Where(x => x.GameId == gameId.Value).ToList();
                var game = _gameService.GetById(gameId.Value);
                ViewBag.SelectedGame = game?.Title;
                return View(filtered);
            }

            return View(allVersions);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();
            var gameVersion = _gameVersionService.GetById(id.Value);
            if (gameVersion == null) return NotFound();
            return View(gameVersion);
        }

        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VersionNumber,GameId,Id")] GameVersion gameVersion)
        {
            // ОВА Е КЛУЧНО: Го тргаме Game објектот од валидација за да работи Create
            ModelState.Remove("Game");

            if (ModelState.IsValid)
            {
                gameVersion.Id = Guid.NewGuid();
                _gameVersionService.Create(gameVersion);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", gameVersion.GameId);
            return View(gameVersion);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var gameVersion = _gameVersionService.GetById(id.Value);
            if (gameVersion == null) return NotFound();
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", gameVersion.GameId);
            return View(gameVersion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("VersionNumber,GameId,Id")] GameVersion gameVersion)
        {
            if (id != gameVersion.Id) return NotFound();

            // И тука го тргаме Game од валидација
            ModelState.Remove("Game");

            if (ModelState.IsValid)
            {
                try
                {
                    _gameVersionService.Update(gameVersion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameVersionExists(gameVersion.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", gameVersion.GameId);
            return View(gameVersion);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var gameVersion = _gameVersionService.GetById(id.Value);
            if (gameVersion == null) return NotFound();
            return View(gameVersion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _gameVersionService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GameVersionExists(Guid id)
        {
            return _gameVersionService.GetById(id) != null;
        }
    }
}
