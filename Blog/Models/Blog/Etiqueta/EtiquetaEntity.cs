using Blog.Models.Blog.Postagem;
using System;
using System.Collections.Generic;

namespace Blog.Models.Blog.Etiqueta
{
    public class EtiquetaEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ICollection<PostagemEtiquetaEntity> PostagemEtiquetas { get; set; }
    }
}
