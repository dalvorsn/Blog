using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminCategoriasListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AdminCategoriasCategoria> Categorias { get; set; }


        public AdminCategoriasListarViewModel()
        {
            TituloPagina = "Categorias - Administrador";
            Categorias = new List<AdminCategoriasCategoria>();
        }
    }

    public class AdminCategoriasCategoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}