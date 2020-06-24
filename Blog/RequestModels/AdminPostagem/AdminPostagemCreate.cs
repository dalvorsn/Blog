using System.Collections.Generic;

namespace Blog.RequestModels.AdminPostagem
{
    public class AdminPostagemCreate
    {
        public string Titulo { get; set; }
        public string Capa { get; set; }
        public int Autor { get; set; }
        public int Categoria { get; set; }
        public string Texto { get; set; }
        public List<int> Etiquetas { get; set; }
    }
}
