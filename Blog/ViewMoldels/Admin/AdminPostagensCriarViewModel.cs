using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminPostagensCriarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutorAdminPostagens> Autores { get; set; }
        public ICollection<CategoriaAdminPostagens> Categorias { get; set; }
        public ICollection<EtiquetaAdminPostagens> Etiquetas { get; set; }
        
        public string Erro { get; set; }
        
        public AdminPostagensCriarViewModel()
        {
            TituloPagina = "Criar nova postagem";
            Autores = new List<AutorAdminPostagens>();
            Categorias = new List<CategoriaAdminPostagens>();
            Etiquetas = new List<EtiquetaAdminPostagens>();
            
        }
    }

    public class AutorAdminPostagens
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    public class CategoriaAdminPostagens
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class EtiquetaAdminPostagens
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}