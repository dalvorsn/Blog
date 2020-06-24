using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminEtiquetasListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<EtiquetaAdminEtiquetas> Etiquetas { get; set; }


        public AdminEtiquetasListarViewModel()
        {
            TituloPagina = "Etiquetas - Administrador";
            Etiquetas = new List<EtiquetaAdminEtiquetas>();
        }
    }

    public class EtiquetaAdminEtiquetas
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
    }
}