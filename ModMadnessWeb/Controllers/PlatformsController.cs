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
    public class PlatformsController : Controller
    {
        private readonly IPlatformService _platformService;

        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        // GET: Platforms
        public IActionResult Index()
        {
            return View(_platformService.GetAll());
        }

        // GET: Platforms/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            var platform = _platformService.GetById(id.Value);
            if (platform == null) return NotFound();

            return View(platform);
        }

        // GET: Platforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platforms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                platform.Id = Guid.NewGuid();
                _platformService.Create(platform);
                return RedirectToAction(nameof(Index));
            }
            return View(platform);
        }

        // GET: Platforms/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var platform = _platformService.GetById(id.Value);
            if (platform == null) return NotFound();

            return View(platform);
        }

        // POST: Platforms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Id")] Platform platform)
        {
            if (id != platform.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _platformService.Update(platform);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformExists(platform.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(platform);
        }

        // GET: Platforms/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var platform = _platformService.GetById(id.Value);
            if (platform == null) return NotFound();

            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _platformService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PlatformExists(Guid id)
        {
            return _platformService.GetById(id) != null;
        }
    }
}
