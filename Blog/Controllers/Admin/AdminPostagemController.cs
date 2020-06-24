using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Categoria;
using Blog.Models.Blog.Etiqueta;
using Blog.Models.Blog.Postagem;
using Blog.RequestModels.AdminPostagem;
using Blog.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Blog.ViewMoldels
{
    [Authorize]
    public class AdminPostagemController : Controller
    {
        private readonly Database _context;
        private readonly PostagemOrmService _postagemOrmService;
        private readonly CategoriaOrmService _categoriaOrmService;
        private readonly AutorOrmService _autorOrmService;
        private readonly EtiquetaOrmService _etiquetaOrmService;

        public AdminPostagemController(Database context, PostagemOrmService PostagemOrmService, CategoriaOrmService CategoriaOrmService, AutorOrmService AutorOrmService, EtiquetaOrmService EtiquetaOrmService)
        {
            _context = context;
            _postagemOrmService = PostagemOrmService;
            _categoriaOrmService = CategoriaOrmService;
            _autorOrmService = AutorOrmService;
            _etiquetaOrmService = EtiquetaOrmService;
        }

        // GET: AdminPostagem
        public IActionResult Index()
        {
            var model = new AdminPostagensListarViewModel();
            foreach (var post in _postagemOrmService.GetAll())
            {
                model.Postagens.Add(
                new PostagemAdminPostagens
                {
                    Id = post.Id,
                    Titulo = post.Titulo,
                    Categoria = post.Categoria.Nome,
                    Autor = post.Autor.Nome
                });
            }

            return View(model);
        }

        // GET: AdminPostagem/Create
        public IActionResult Create()
        {
            var model = new AdminPostagensCriarViewModel();

            foreach(var autor in _autorOrmService.GetAll())
            {
                model.Autores.Add(new AutorAdminPostagens
                {
                    Id = autor.Id,
                    Nome = autor.Nome
                });
            }

            foreach(var categoria in _categoriaOrmService.GetAll())
            {
                model.Categorias.Add(new CategoriaAdminPostagens
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                });
            }

            foreach (var etiqueta in _etiquetaOrmService.GetAll())
            {
                model.Etiquetas.Add(new EtiquetaAdminPostagens
                {
                    Id = etiqueta.Id,
                    Nome = etiqueta.Nome
                });
            }

            model.Erro = (string)TempData["erro-msg"];

            return View(model);
        }

        // POST: AdminPostagem/Create
        [HttpPost]
        public IActionResult Create(AdminPostagemCreate request)
        {
            var titulo = request.Titulo;
            var categoria = request.Categoria;
            var autor = request.Autor;
            var texto = request.Texto;
            var capa = request.Capa;
            var etiquetas = request.Etiquetas;

            try
            {
                _postagemOrmService.Create(titulo, categoria, autor, texto, capa, etiquetas);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminPostagem/Edit/5
        public IActionResult Edit(int id)
        {
            var model = new AdminPostagensEditarViewModel
            {
                Erro = (string)TempData["erro-msg"]
            };

            foreach (var autor in _autorOrmService.GetAll())
            {
                model.Autores.Add(new AutorAdminPostagens
                {
                    Id = autor.Id,
                    Nome = autor.Nome
                });
            }

            foreach (var categoria in _categoriaOrmService.GetAll())
            {
                model.Categorias.Add(new CategoriaAdminPostagens
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                });
            }

            foreach (var etiqueta in _etiquetaOrmService.GetAll())
            {
                model.Etiquetas.Add(new EtiquetaAdminPostagens
                {
                    Id = etiqueta.Id,
                    Nome = etiqueta.Nome
                });
            }

            var postagem = _postagemOrmService.Get(id);
            model.Id = postagem.Id;
            model.AutorId = postagem.AutorId;
            model.CategoriaId = postagem.CategoriaId;
            model.Titulo = postagem.Titulo;
            model.Texto = postagem.Revisoes.OrderByDescending(r => r.Versao).Last().Texto;
            model.Capa = postagem.UrlCapa;

            foreach(var etiqueta in postagem.PostagemEtiquetas)
            {
                model.EtiquetasPostagem.Add(etiqueta.EtiquetaId);
            }

            return View(model);
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
            var etiquetas = request.Etiquetas;

            try
            {
                _postagemOrmService.Edit(id, titulo, categoria, autor, texto, capa, etiquetas);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminPostagem/Delete/5
        public IActionResult Delete(int id)
        {
            var postagem = _postagemOrmService.Get(id);
            var model = new AdminPostagensRemoverViewModel
            {
                Id = postagem.Id,
                Titulo = postagem.Titulo,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
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
