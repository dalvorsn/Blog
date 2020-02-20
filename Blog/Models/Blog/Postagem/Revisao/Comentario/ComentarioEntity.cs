using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Blog.Postagem.Revisao
{
    public class ComentarioEntity
    {
        public RevisaoEntity Revisao;
        public string Texto;
        public string Autor;
        public DateTime Data;
    }
}
