using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Categoria;
using Blog.Models.Blog.Etiqueta;
using Blog.Models.Blog.Postagem;
using Blog.Models.Blog.Postagem.Revisao;
using Blog.Models.Blog.Postagem.Revisao.Classificacao;
using Blog.Models.Blog.PostagemEtiqueta;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<PostagemEtiquetaEntity> PostagemEtiquetas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=blog;user=root;password=123456");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostagemEtiquetaEntity>().HasKey(pe => new { pe.EtiquetaId, pe.PostagemId });
        }

        public void CreateFakeData()
        {
            // Autores
            this.Autores.Add(new AutorEntity { Nome = "João Costa", FotoURL = "https://www.stf.jus.br/arquivo/cms/bancoImagemSco/bancoImagemSco_AP_344446.jpg" });

            // Categorias
            this.Categorias.Add(new CategoriaEntity { Nome = "Programação" });
            this.Categorias.Add(new CategoriaEntity { Nome = "Banco de Dados" });
            this.Categorias.Add(new CategoriaEntity { Nome = "Infraestrutura" });

            // Etiquetas
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#8a71af", Nome = "DevOps", DataCriacao = DateTime.Now });
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#719f67", Nome = "Database", DataCriacao = DateTime.Now });
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#f6a6b2", Nome = "Front-end", DataCriacao = DateTime.Now });
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#fe5e51", Nome = "Back-end", DataCriacao = DateTime.Now });

            this.SaveChanges();
        }
    }
}
