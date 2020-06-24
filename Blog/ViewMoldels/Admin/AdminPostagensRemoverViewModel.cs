using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminPostagensRemoverViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }
        
        public string Titulo { get; set; }

        public string Erro { get; set; }
        
        public AdminPostagensRemoverViewModel()
        {
            TituloPagina = "Remover Postagem: ";
        }
    }
}