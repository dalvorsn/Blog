using Blog.Models.Blog.Postagem;
using Blog.RequestModels.AdminPostagem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.ViewMoldels
{
    [Authorize]
    public class AdminPostagemController : Controller
    {
        private readonly Database _context;
        private readonly PostagemOrmService _postagemOrmService;

        public AdminPostagemController(Database context, PostagemOrmService PostagemOrmService)
        {
            _context = context;
            _postagemOrmService = PostagemOrmService;
        }

        // GET: AdminPostagem
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminPostagem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPostagem/Create
        [HttpPost]
        public IActionResult Create(AdminPostagemCreate request)
        {
            var titulo = request.Titulo;
            var categoria = request.CategoriaId;
            var autor = request.AutorId;
            var texto = request.Texto;
            var capa = request.UrlCapa;

            try
            {
                _postagemOrmService.Create(titulo, categoria, autor, texto, capa);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminPostagem/Details/5
        public IActionResult Details(int? id)
        {
            return View();
        }

        // GET: AdminPostagem/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: AdminPostagem/Edit/5
        [HttpPost]
        public IActionResult Edit(AdminPostagemEdit request)
        {
            var id = request.Id;
            var titulo = request.Titulo;
            var categoria = request.CategoriaId;
            var autor = request.AutorId;
            var texto = request.Texto;
            var capa = request.UrlCapa;

            try
            {
                _postagemOrmService.Edit(id, titulo, categoria, autor, texto, capa);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminPostagem/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AdminPostagem/Delete/5
        [HttpPost]
        public IActionResult Delete(AdminPostagemDelete request)
        {
            var id = request.Id;

            try
            {
                _postagemOrmService.Delete(id);
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
