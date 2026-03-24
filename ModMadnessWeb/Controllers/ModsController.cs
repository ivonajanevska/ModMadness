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
    public class ModsController : Controller
    {
        private readonly IModService _modService;
        private readonly IGameService _gameService;

        public ModsController(IModService modService, IGameService gameService)
        {
            _modService = modService;
            _gameService = gameService;
        }

        // ПОПРАВЕН INDEX: Прифаќа Guid? за филтрирање и нема дупликати
        public IActionResult Index(Guid? gameId)
        {
            var allMods = _modService.GetAll();

            if (gameId.HasValue)
            {
                // Ги филтрираме модовите само за таа игра
                var filteredMods = allMods.Where(m => m.GameId == gameId.Value).ToList();

                // Земаме име на играта за да го прикажеме во насловот на страната
                var game = _gameService.GetById(gameId.Value);
                ViewBag.SelectedGame = game?.Title;

                return View(filteredMods);
            }

            return View(allMods);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();
            var mod = _modService.GetById(id.Value);
            if (mod == null) return NotFound();
            return View(mod);
        }

        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,IsEnabled,GameId,Id")] Mod mod)
        {
            // Го тргаме објектот Game од валидација за да помине ModelState.IsValid
            ModelState.Remove("Game");

            if (ModelState.IsValid)
            {
                mod.Id = Guid.NewGuid();
                _modService.Create(mod);
                return RedirectToAction(nameof(Index));
            }

            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", mod.GameId);
            return View(mod);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var mod = _modService.GetById(id.Value);
            if (mod == null) return NotFound();

            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", mod.GameId);
            return View(mod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Description,IsEnabled,GameId,Id")] Mod mod)
        {
            if (id != mod.Id) return NotFound();

            ModelState.Remove("Game"); // Исто и тука за едит

            if (ModelState.IsValid)
            {
                try
                {
                    _modService.Update(mod);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModExists(mod.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", mod.GameId);
            return View(mod);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var mod = _modService.GetById(id.Value);
            if (mod == null) return NotFound();
            return View(mod);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _modService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ModExists(Guid id)
        {
            return _modService.GetById(id) != null;
        }
    }
}
