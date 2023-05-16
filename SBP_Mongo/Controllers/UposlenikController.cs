using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class UposlenikController : Controller
    {
        private readonly UposlenikService _UposlenikService;
        private readonly PozicijaService _PozicijaService;

        public UposlenikController(UposlenikService UposlenikService, PozicijaService pozicijaService)
        {
            _UposlenikService = UposlenikService;
            _PozicijaService = pozicijaService;
        }
        // GET: UposlenikController
        public async Task<IActionResult> Index()
        {
            var uposlenici = await _UposlenikService.GetAsync();
            return View(uposlenici);
        }

        // GET: UposlenikController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _UposlenikService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var Uposlenik = await _UposlenikService.GetAsync(id);
            if (Uposlenik == null)
            {
                return NotFound();
            }

            return View(Uposlenik);
        }

        // GET: UposlenikController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Pozicija = await _PozicijaService.GetAsync();
            return View();
        }

        // POST: UposlenikController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ime", "Prezime", "Pozicija", "Adresa", "BrojMobitela", "Jmbg", "Email", "Naziv")] Uposlenik Uposlenik)
        {
            if (ModelState.IsValid && Uposlenik.Pozicija != null)
            {
                await _UposlenikService.CreateAsync(Uposlenik);
                return RedirectToAction(nameof(Index));
            }
            return View(Uposlenik);
        }

        // GET: UposlenikController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _UposlenikService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var Uposlenik = await _UposlenikService.GetAsync(id);

            if (Uposlenik == null)
            {
                return NotFound();
            }
            ViewBag.Pozicija = await _PozicijaService.GetAsync();

            return View(Uposlenik);
        }

        // POST: UposlenikController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id", "Ime", "Prezime", "Pozicija", "Adresa", "BrojMobitela", "Jmbg", "Email", "Naziv")] Uposlenik Uposlenik)
        {
            if (id != Uposlenik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _UposlenikService.UpdateAsync(Uposlenik.Id, Uposlenik);

                return RedirectToAction(nameof(Index));
            }
            return View(Uposlenik);
        }

        // GET: UposlenikController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Uposlenik = await _UposlenikService.GetAsync(id);
            if (Uposlenik == null)
            {
                return NotFound();
            }

            return View(Uposlenik);
        }

        // POST: Uposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var Uposlenik = await _UposlenikService.GetAsync(id);
            if (Uposlenik != null)
            {
                await _UposlenikService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
