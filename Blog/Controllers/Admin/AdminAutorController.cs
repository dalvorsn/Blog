using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Models.Blog.Autor;
using Blog.RequestModels.AdminAutor;

namespace Blog.ViewMoldels
{
    public class AdminAutorController : Controller
    {
        private readonly Database _context;
        private readonly AutorOrmService _autorOrmService;

        public AdminAutorController(Database context, AutorOrmService AutorOrmService)
        {
            _context = context;
            _autorOrmService = AutorOrmService;
        }

        // GET: AdminAutor
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminAutor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminAutor/Create
        [HttpPost]
        public IActionResult Create(AdminAutorCreate request)
        {
            var nome = request.Nome;
            var url = request.FotoURL;

            try {
                _autorOrmService.Create(nome, url);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminAutor/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: AdminAutor/Edit/5
        [HttpPost]
        public IActionResult Edit(AdminAutorEdit request)
        {
            var id = request.Id;
            var nome = request.Nome;
            var url = request.FotoURL;

            try {
                _autorOrmService.Edit(id, nome, url);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminAutor/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AdminAutor/Delete/5
        [HttpPost]
        public IActionResult Delete(AdminAutorDelete request)
        {
            var id = request.Id;

            try {
                _autorOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
