﻿using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Categoria;
using Blog.Models.Blog.Postagem.Revisao;
using Blog.Models.Blog.Postagem.Revisao.Classificacao;
using System.Collections.Generic;

namespace Blog.Models.Blog.Postagem
{
    public class PostagemEntity
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string UrlCapa { get; set; }
        public virtual AutorEntity Autor { get; set; }
        public virtual CategoriaEntity Categoria { get; set; }
        public virtual ICollection<PostagemEtiquetaEntity> PostagemEtiquetas { get; set; }
        public virtual ICollection<RevisaoEntity> Revisoes { get; set; }
        public virtual ICollection<ClassificacaoEntity> Classificacoes { get; set; }
    }
}
