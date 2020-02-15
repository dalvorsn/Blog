using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Categoria;

namespace Blog.Models.Blog.Postagem
{
    public class PostagemEntity
    {
        public string Titulo;
        public AutorEntity Autor;
        public CategoriaEntity Categoria;
    }
}
