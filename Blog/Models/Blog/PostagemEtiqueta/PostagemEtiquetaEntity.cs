using Blog.Models.Blog.Etiqueta;
using Blog.Models.Blog.Postagem;

namespace Blog.Models.Blog.PostagemEtiqueta
{
    public class PostagemEtiquetaEntity
    {
        public int EtiquetaId { get; set; }
        public int PostagemId { get; set; }
        public virtual PostagemEntity Postagem { get; set; }
        public virtual EtiquetaEntity Etiqueta { get; set; }
    }
}
