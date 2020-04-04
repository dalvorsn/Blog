using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.Models;
using System.Linq;
using Blog.ViewMoldels.Home;
using Blog.Models.Blog.Categoria;
using Blog.Models.Blog.Postagem;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoriaOrmService _categoriaOrmService;
        private readonly PostagemOrmService _postagemOrmService;

        public HomeController(
            ILogger<HomeController> logger,
            CategoriaOrmService categoriaOrmService,
            PostagemOrmService postagemOrmService
        ){
            _logger = logger;
            _categoriaOrmService = categoriaOrmService;
            _postagemOrmService = postagemOrmService;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            foreach(var post in _postagemOrmService.GetAll())
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

            foreach(var post in _postagemOrmService.GetMostPopular())
            {
                var postagem = new PostagemMostPopularHomeIndex();
                postagem.Titulo = post.Titulo;
                postagem.Autor = post.Autor;

                model.PopularPosts.Add(postagem);
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
