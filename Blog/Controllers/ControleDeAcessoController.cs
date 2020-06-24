using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models.ControleDeAcesso;
using Blog.RequestModels.ControleDeAcesso;
using Blog.ViewModels.ControleDeAcesso;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class ControleDeAcessoController : Controller
    {
        private readonly ControleDeAcessoService _controleDeAcessoService;

        public ControleDeAcessoController(
            ControleDeAcessoService controleDeAcessoService
        )
        {
            _controleDeAcessoService = controleDeAcessoService;
        }

        [HttpGet]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            ControleDeAcessoLoginViewModel model = new ControleDeAcessoLoginViewModel();

            model.MensagemRegistro = (string) TempData["registrar-msg"];
            model.MensagemLogin = (string) TempData["login-msg"];
            model.Destino = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<RedirectResult> Login(ControleDeAcessoLoginRequestModel request)
        {
            var usuario = request.Usuario;
            var senha = request.Senha;
            var destinoAposSucessoNoLogin = request.Destino ?? "AdminEtiqueta";

            var loginUrl = "acesso/login?ReturnUrl=" + request.Destino;

            if (usuario == null) {
                TempData["login-msg"] = "Por favor informe o nome de usuário";
                return Redirect(loginUrl);
            }

            if (senha == null) {
                TempData["login-msg"] = "Por favor informe a senha";
                return Redirect(loginUrl);
            }

            try {
                await _controleDeAcessoService.AutenticarUsuario(usuario, senha);
                return Redirect(destinoAposSucessoNoLogin);
            } catch (Exception exception) {
                TempData["login-msg"] = exception.Message;
                return Redirect(loginUrl);
            }
        }

        [HttpGet]
        public RedirectToActionResult Logout()
        {
            _controleDeAcessoService.DeslogarUsuario();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            ControleDeAcessoRegistrarViewModel model = new ControleDeAcessoRegistrarViewModel();

            model.ErrosRegistro = (string[]) TempData["erros-registro"];
            model.Erro = (string) TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Registrar(ControleDeAcessoRegistrarRequestModel request)
        {
            var email = request.Email;
            var senha = request.Senha;
            var token = request.Token ?? "";

            if (email == null) {
                TempData["erro-msg"] = "Por favor informe um email";
                return RedirectToAction("Registrar");
            }

            if (senha == null) {
                TempData["erro-msg"] = "Por favor informe uma senha";
                return RedirectToAction("Registrar");
            }

            if (!token.Equals("BLOG-PWA-2020")) {
                TempData["erro-msg"] = "Token Inválido!";
                return RedirectToAction("Registrar");
            }

            try {
                await _controleDeAcessoService.RegistrarUsuario(email, senha);

                TempData["registrar-msg"] = "Usuário Registrado!";
                return RedirectToAction("Login");
                
            } catch (RegistrarUsuarioException exception) {
                var listaDeErros = new List<string>();

                foreach (var erro in exception.Erros) {
                    listaDeErros.Add(erro.Description);
                }

                TempData["erros-registro"] = listaDeErros;

                return RedirectToAction("Registrar");
            }
        }
    }
}