using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class PozicijaController : Controller
    {
        private readonly PozicijaService _pozicijaService;

        public PozicijaController(PozicijaService pozicijaService)
        {
            _pozicijaService = pozicijaService;
        }
        // GET: PozicijaController
        public async Task<IActionResult> Index()
        {
            var pozicije = await _pozicijaService.GetAsync();
            return View(pozicije);
        }

        // GET: PozicijaController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _pozicijaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var pozicija = await _pozicijaService.GetAsync(id);
            if (pozicija == null)
            {
                return NotFound();
            }

            return View(pozicija);
        }

        // GET: PozicijaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PozicijaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naziv")] Pozicija pozicija)
        {
            if (ModelState.IsValid)
            {
                await _pozicijaService.CreateAsync(pozicija);
                return RedirectToAction(nameof(Index));
            }
            return View(pozicija);
        }

        // GET: PozicijaController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _pozicijaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var pozicija = await _pozicijaService.GetAsync(id);
            if (pozicija == null)
            {
                return NotFound();
            }

            return View(pozicija);
        }

        // POST: PozicijaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Naziv")] Pozicija pozicija)
        {
            if (id != pozicija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _pozicijaService.UpdateAsync(pozicija.Id, pozicija);

                return RedirectToAction(nameof(Index));
            }
            return View(pozicija);
        }

        // GET: PozicijaController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozicija = await _pozicijaService.GetAsync(id);
            if (pozicija == null)
            {
                return NotFound();
            }

            return View(pozicija);
        }

        // POST: Pozicija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var pozicija = await _pozicijaService.GetAsync(id);
            if (pozicija != null)
            {
                await _pozicijaService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
