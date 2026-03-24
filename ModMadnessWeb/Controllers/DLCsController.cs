using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModMadnessDomain.Domain;
using ModMadnessRepository.Data;
using ModMadnessService.Implementation;
using ModMadnessService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModMadnessWeb.Controllers
{
    public class DLCsController : Controller
    {
        private readonly IDLCService _dlcService;
        private readonly IGameService _gameService; // Ни треба за да ги избереме игрите во Dropdown

        public DLCsController(IDLCService dlcService, IGameService gameService)
        {
            _dlcService = dlcService;
            _gameService = gameService;
        }

        // GET: DLCs
        public IActionResult Index(Guid? gameId)
        {
            var allDLCs = _dlcService.GetAll();

            if (gameId.HasValue)
            {
                // Ги филтрираме модовите само за таа игра
                var filteredMods = allDLCs.Where(m => m.GameId == gameId.Value).ToList();



                // Земаме име на играта за да го прикажеме во насловот на страната
                var game = _gameService.GetById(gameId.Value);
                ViewBag.SelectedGame = game?.Title;

                return View(filteredMods);
            }

            return View(allDLCs);
        }

        // GET: DLCs/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            var dlc = _dlcService.GetById(id.Value);
            if (dlc == null) return NotFound();

            return View(dlc);
        }

        // GET: DLCs/Create
        public IActionResult Create()
        {
            // Го користиме GameService за да ја наполниме листата на игри
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title");
            return View();
        }

        // POST: DLCs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,IsInstalled,GameId,Id")] DLC dlc)
        {
            ModelState.Remove("Game");
            if (ModelState.IsValid)
            {
                dlc.Id = Guid.NewGuid();
                _dlcService.Create(dlc);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", dlc.GameId);
            return View(dlc);
        }

        // GET: DLCs/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var dlc = _dlcService.GetById(id.Value);
            if (dlc == null) return NotFound();

            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", dlc.GameId);
            return View(dlc);
        }

        // POST: DLCs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,IsInstalled,GameId,Id")] DLC dlc)
        {
            if (id != dlc.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dlcService.Update(dlc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DLCExists(dlc.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_gameService.GetAll(), "Id", "Title", dlc.GameId);
            return View(dlc);
        }

        // GET: DLCs/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var dlc = _dlcService.GetById(id.Value);
            if (dlc == null) return NotFound();

            return View(dlc);
        }

        // POST: DLCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _dlcService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DLCExists(Guid id)
        {
            return _dlcService.GetById(id) != null;
        }
    }
}
