﻿using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class DodjelaVozilaController : Controller
    {
        private readonly DodjelaVozilaService _DodjelaVozilaService;
        private readonly UposlenikService _UposlenikService;
        private readonly VoziloService _VoziloService;

        public DodjelaVozilaController(DodjelaVozilaService DodjelaVozilaService, UposlenikService uposlenikService, VoziloService voziloService)
        {
            _DodjelaVozilaService = DodjelaVozilaService;
            _UposlenikService = uposlenikService;
            _VoziloService = voziloService;
        }
        // GET: DodjelaVozilaController
        public async Task<IActionResult> Index()
        {
            var vozila = await _DodjelaVozilaService.GetAsync();
            return View(vozila);
        }

        // GET: DodjelaVozilaController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _DodjelaVozilaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var DodjelaVozila = await _DodjelaVozilaService.GetAsync(id);
            if (DodjelaVozila == null)
            {
                return NotFound();
            }

            return View(DodjelaVozila);
        }

        // GET: DodjelaVozilaController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Uposlenik = await _UposlenikService.GetAsync();
            ViewBag.Vozilo = await _VoziloService.GetAsync();
            return View();
        }

        // POST: DodjelaVozilaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] DodjelaVozila dodjelaVozila)
        {
            if (dodjelaVozila.Uposlenik != null && dodjelaVozila.Vozilo != null)
            {
                Uposlenik u = await _UposlenikService.GetAsync(dodjelaVozila.Uposlenik);
                Vozilo v = await _VoziloService.GetAsync(dodjelaVozila.Vozilo);
                dodjelaVozila.UposlenikO = u;
                dodjelaVozila.VoziloO = v;
            }
            await _DodjelaVozilaService.CreateAsync(dodjelaVozila);
            return RedirectToAction(nameof(Index));

        }

        // GET: DodjelaVozilaController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _DodjelaVozilaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var DodjelaVozila = await _DodjelaVozilaService.GetAsync(id);
            if (DodjelaVozila == null)
            {
                return NotFound();
            }
            ViewBag.Uposlenik = await _UposlenikService.GetAsync();
            ViewBag.Vozilo = await _VoziloService.GetAsync();

            return View(DodjelaVozila);
        }

        // POST: DodjelaVozilaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DodjelaVozila DodjelaVozila)
        {
            if (id != DodjelaVozila.Id)
            {
                return NotFound();
            }


            if (DodjelaVozila.Uposlenik != null && DodjelaVozila.Vozilo != null)
            {
                Uposlenik u = await _UposlenikService.GetAsync(DodjelaVozila.Uposlenik);
                Vozilo v = await _VoziloService.GetAsync(DodjelaVozila.Vozilo);
                DodjelaVozila.UposlenikO = u;
                DodjelaVozila.VoziloO = v;
            }

            await _DodjelaVozilaService.UpdateAsync(DodjelaVozila.Id, DodjelaVozila);

            return RedirectToAction(nameof(Index));

            return View(DodjelaVozila);
        }

        // GET: DodjelaVozilaController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DodjelaVozila = await _DodjelaVozilaService.GetAsync(id);
            if (DodjelaVozila == null)
            {
                return NotFound();
            }

            return View(DodjelaVozila);
        }

        // POST: DodjelaVozila/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var DodjelaVozila = await _DodjelaVozilaService.GetAsync(id);
            if (DodjelaVozila != null)
            {
                await _DodjelaVozilaService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
