using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class ModelController : Controller
    {
        private readonly ModelService _modelService;

        public ModelController(ModelService modelService)
        {
            _modelService = modelService;
        }
        // GET: ModelController
        public async Task<IActionResult> Index()
        {
            var modeli = await _modelService.GetAsync();
            return View(modeli);
        }

        // GET: ModelController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _modelService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var model = await _modelService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: ModelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naziv,IdMarke")] Model model)
        {
            if (ModelState.IsValid)
            {
                await _modelService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ModelController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _modelService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var model = await _modelService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: ModelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Naziv,IdMarke")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _modelService.UpdateAsync(model.Id, model);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ModelController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _modelService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var model = await _modelService.GetAsync(id);
            if (model != null)
            {
                await _modelService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
