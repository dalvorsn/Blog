using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Models.Blog.Autor;
using Blog.RequestModels.AdminAutor;
using Microsoft.AspNetCore.Authorization;
using Blog.ViewModels.Admin;

namespace Blog.ViewMoldels
{
    [Authorize]
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
            var model = new AdminAutoresListarViewModel();
            foreach( var autor in _autorOrmService.GetAll())
            {
                model.Autores.Add(new AdminAutoresAutor
                {
                    Id = autor.Id,
                    Nome = autor.Nome,
                    FotoURL = autor.FotoURL
                });
            }
            
            return View(model);
        }

        // GET: AdminAutor/Create
        public IActionResult Create()
        {
            var model = new AdminAutoresCriarViewModel
            {
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
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
        public IActionResult Edit(int id)
        {
            var autor = _autorOrmService.Get(id);
            var model = new AdminAutoresEditarViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome,
                FotoURL = autor.FotoURL,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
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
        public IActionResult Delete(int id)
        {
            var autor = _autorOrmService.Get(id);
            var model = new AdminAutoresRemoverViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
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
