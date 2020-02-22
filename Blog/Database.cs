using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Categoria;
using Blog.Models.Blog.Etiqueta;
using Blog.Models.Blog.Postagem;
using Blog.Models.Blog.Postagem.Revisao;
using Blog.Models.Blog.Postagem.Revisao.Classificacao;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    public class Database : DbContext
    {
        public DbSet<PostagemEntity> Postagems { get; set; }
        public DbSet<EtiquetaEntity> Etiquetas { get; set; }
        public DbSet<RevisaoEntity> Revisoes { get; set; }
        public DbSet<ComentarioEntity> Comentarios { get; set; }
        public DbSet<ClassificacaoEntity> Classificacoes { get; set; }
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<AutorEntity> Autores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=blog;user=root;password=123456");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
