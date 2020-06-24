using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminEtiquetasCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }


        public AdminEtiquetasCriarViewModel()
        {
            TituloPagina = "Criar Etiqueta: ";
        }
    }
}