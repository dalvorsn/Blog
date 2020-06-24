using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminEtiquetasEditarViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        public string Cor { get; set; }


        public string Erro { get; set; }
        
        
        public AdminEtiquetasEditarViewModel()
        {
            TituloPagina = "Editar Etiqueta: ";
        }
    }
}