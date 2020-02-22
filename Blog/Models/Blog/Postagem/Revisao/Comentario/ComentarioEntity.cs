using System;

namespace Blog.Models.Blog.Postagem.Revisao
{
    public class ComentarioEntity
    {
        public int Id { get; set; }
        public int RevisaoId { get; set; }
        public virtual RevisaoEntity Revisao { get; set; }
        public string Texto { get; set; }
        public string Autor { get; set; }
        public DateTime Data { get; set; }
    }
}
