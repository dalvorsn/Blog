using Microsoft.AspNetCore.Mvc;
using Blog.Models.Blog.Categoria;
using Blog.RequestModels.AdminCategoria;
using System;
using Microsoft.AspNetCore.Authorization;
using Blog.ViewModels.Admin;

namespace Blog.ViewMoldels
{
    [Authorize]
    public class AdminCategoriaController : Controller
    {
        private readonly Database _context;
        private readonly CategoriaOrmService _categoriaOrmService;

        public AdminCategoriaController(Database context, CategoriaOrmService categoriaOrmService)
        {
            _context = context;
            _categoriaOrmService = categoriaOrmService;
        }

        // GET: AdminCategoria
        public IActionResult Index()
        {   
            var model = new AdminCategoriasListarViewModel();
            foreach (var categoria in _categoriaOrmService.GetAll())
            {
                model.Categorias.Add(new AdminCategoriasCategoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                });
            }

            return View(model);
        }

        // GET: AdminCategoria/Create
        public IActionResult Create()
        {
            var model = new AdminCategoriasCriarViewModel
            {
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
        }

        // POST: AdminCategoria/Create
        [HttpPost]
        public IActionResult Create(AdminCategoriaCreate request)
        {
            var nome = request.Nome;

            try {
                _categoriaOrmService.Create(nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCategoria/Edit/5
        public IActionResult Edit(int id)
        {
            var categoria = _categoriaOrmService.Get(id);
            var model = new AdminCategoriasEditarViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
        }

        // POST: AdminCategoria/Edit/5
        [HttpPost]
        public IActionResult Edit(AdminCategoriaEdit request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                _categoriaOrmService.Edit(id, nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCategoria/Delete/5
        public IActionResult Delete(int id)
        {
            
            var categoria = _categoriaOrmService.Get(id);
            var model = new AdminCategoriasRemoverViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
        }

        // POST: AdminCategoria/Delete/5
        [HttpPost]
        public IActionResult Delete(AdminCategoriaDelete request)
        {
            var id = request.Id;

            try {
                _categoriaOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
