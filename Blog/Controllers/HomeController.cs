using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Blog.ViewMoldels.Home;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var db = new Database();
            var posts = db.Postagems
                .Include(c => c.Categoria)
                .Include(r => r.Revisoes)
                .Include(a => a.Autor)
                .Select( p => new
                {
                    p,
                    Revisoes = p.Revisoes.OrderByDescending( r => r.Versao ).Last()
                })
                .AsEnumerable()
                .Select(e => e.p)
                .ToList();

            HomeIndexViewModel model = new HomeIndexViewModel();
            foreach(var post in posts)
            {
                var postagem = new PostagemHomeIndex();
                postagem.Titulo = post.Titulo;
                postagem.UrlCapa = post.UrlCapa;

                var revisao = post.Revisoes.FirstOrDefault();
                if(revisao != null)
                {
                    postagem.Id = revisao.Id;
                    postagem.Texto = revisao.Texto;
                    postagem.UltimaAtualizacao = revisao.Data;
                    postagem.Autor = post.Autor;
                    model.Postagens.Add(postagem);
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
