using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Blog.Postagem.Revisao
{
    public class RevisaoEntity
    {
        public int Id { get; set; }
        public int PostagemId { get; set; }
        public virtual PostagemEntity Postagem { get; set; }
        [MinLength(400)]
        public string Texto { get; set; }
        public int Versao { get; set; }
        public DateTime Data { get; set; }
    }
}
