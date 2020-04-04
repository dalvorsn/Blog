using Blog.Models.Blog.Etiqueta;

namespace Blog.Models.Blog.Postagem
{
    public class PostagemEtiquetaEntity
    {
        public int EtiquetaId { get; set; }
        public int PostagemId { get; set; }
        public virtual PostagemEntity Postagem { get; set; }
        public virtual EtiquetaEntity Etiqueta { get; set; }
    }
}
