using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminAutoresRemoverViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public string Erro { get; set; }
        
        public AdminAutoresRemoverViewModel()
        {
            TituloPagina = "Remover Autor: ";
        }
    }
}