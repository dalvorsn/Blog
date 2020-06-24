using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Models.Blog.Etiqueta;
using Blog.RequestModels.AdminEtiqueta;
using Microsoft.AspNetCore.Authorization;
using Blog.ViewModels.Admin;

namespace Blog.ViewMoldels
{
    [Authorize]
    public class AdminEtiquetaController : Controller
    {
        private readonly Database _context;
        private readonly EtiquetaOrmService _EtiquetaOrmService;

        public AdminEtiquetaController(Database context, EtiquetaOrmService EtiquetaOrmService)
        {
            _context = context;
            _EtiquetaOrmService = EtiquetaOrmService;
        }

        // GET: AdminEtiqueta
        public IActionResult Index()
        {
            var model = new AdminEtiquetasListarViewModel();
            
            foreach(var e in _EtiquetaOrmService.GetAll())
            {
                var etiqueta = new EtiquetaAdminEtiquetas();
                etiqueta.Id = e.Id;
                etiqueta.Nome = e.Nome;
                etiqueta.Cor = e.Cor;

                model.Etiquetas.Add(etiqueta);
            }

            return View(model);
        }

        // GET: AdminEtiqueta/Create
        public IActionResult Create()
        {
            var model = new AdminEtiquetasCriarViewModel
            {
                Erro = (string)TempData["erro-msg"]
            };
            return View(model);
        }

        // POST: AdminEtiqueta/Create
        [HttpPost]
        public IActionResult Create(AdminEtiquetaCreate request)
        {
            var nome = request.Nome;
            var cor = request.Cor;

            try
            {
                _EtiquetaOrmService.Create(nome, cor);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminEtiqueta/Edit/5
        public IActionResult Edit(int id)
        {
            var etiqueta = _EtiquetaOrmService.Get(id);
            var model = new AdminEtiquetasEditarViewModel
            {
                Id = etiqueta.Id,
                Nome = etiqueta.Nome,
                Cor = etiqueta.Cor,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
        }

        // POST: AdminEtiqueta/Edit/5
        [HttpPost]
        public IActionResult Edit(AdminEtiquetaEdit request)
        {
            var id = request.Id;
            var nome = request.Nome;
            var cor = request.Cor;

            try
            {
                _EtiquetaOrmService.Edit(id, nome, cor);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminEtiqueta/Delete/5
        public IActionResult Delete(int id)
        {
            var etiqueta = _EtiquetaOrmService.Get(id);
            var model = new AdminEtiquetasRemoverViewModel
            {
                Id = etiqueta.Id,
                Nome = etiqueta.Nome,
                Cor = etiqueta.Cor,
                Erro = (string)TempData["erro-msg"]
        };

            return View(model);
        }

        // POST: AdminEtiqueta/Delete/5
        [HttpPost]
        public IActionResult Delete(AdminEtiquetaDelete request)
        {
            var id = request.Id;

            try
            {
                _EtiquetaOrmService.Delete(id);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
