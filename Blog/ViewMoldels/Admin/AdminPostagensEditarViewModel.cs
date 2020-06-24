using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminPostagensEditarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutorAdminPostagens> Autores { get; set; }
        public ICollection<CategoriaAdminPostagens> Categorias { get; set; }
        public ICollection<EtiquetaAdminPostagens> Etiquetas { get; set; }
        public ICollection<int> EtiquetasPostagem { get; set; }

        public int Id { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string Titulo { get; set; }
        public string Capa { get; set; }
        public string Texto { get; set; }
        public string Erro { get; set; }
        
        public AdminPostagensEditarViewModel()
        {
            TituloPagina = "Editar postagem";
            Autores = new List<AutorAdminPostagens>();
            Categorias = new List<CategoriaAdminPostagens>();
            Etiquetas = new List<EtiquetaAdminPostagens>();
            EtiquetasPostagem = new List<int>();
        }
    }
}