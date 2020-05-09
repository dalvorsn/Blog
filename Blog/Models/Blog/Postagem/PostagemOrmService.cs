using Blog.Models.Blog.Postagem.Revisao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly Database db;
        public PostagemOrmService(Database database)
        {
            this.db = database;
        }

        public int GetUltimaVersaoRevisao(int postagemId)
        {
            var revisao = this.db.Postagems
              .Include(r => r.Revisoes)
              .Where( p => p.Id == postagemId )
              .Select(p => p.Revisoes.OrderByDescending(r => r.Versao).Last())
              .FirstOrDefault();

            if (revisao == null)
                return 0;
              
            return revisao.Versao;
        }

        public List<PostagemEntity> GetAll()
        {
            return this.db.Postagems
               .Include(c => c.Categoria)
               .Include(r => r.Revisoes)
               .Include(a => a.Autor)
               .Select(p => new
               {
                   p,
                   Revisoes = p.Revisoes.OrderByDescending(r => r.Versao).Last()
               })
               .AsEnumerable()
               .Select(e => e.p)
               .ToList();
        }

        public List<PostagemEntity> GetMostPopular()
        {
            return this.db.Postagems
                .Include(a => a.Autor)
                .OrderByDescending(p => p.Classificacoes.Count)
                .Take(6)
                .ToList();
        }

        internal PostagemEntity Create(string titulo, int categoriaId, int autorId, string texto, string capa)
        {
            var categoria = this.db.Categorias.Find(categoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            var autor = this.db.Autores.Find(autorId);
            if (autor == null)
                throw new Exception("Autor não encontrado.");


            var postagem = new PostagemEntity
            {
                AutorId = autorId,
                CategoriaId = categoriaId,
                Titulo = titulo,
                UrlCapa = capa
            };

            this.db.Postagems.Add(postagem);
            this.db.SaveChanges();

            var revisao = new RevisaoEntity 
            { 
                Texto = texto, 
                Data = DateTime.Now,
                Versao = 1,
            };

            postagem.Revisoes.Add(revisao);
            this.db.SaveChanges();

            return postagem;
        }

        internal PostagemEntity Edit(int id, string titulo, int categoriaId, int autorId, string texto, string capa)
        {
            var postagem = this.db.Postagems.Find(id);
            if(postagem == null)
                throw new Exception("Postagem não encontrada.");

            var categoria = this.db.Categorias.Find(categoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            var autor = this.db.Autores.Find(autorId);
            if (autor == null)
                throw new Exception("Autor não encontrado.");

            postagem.Titulo = titulo;
            postagem.CategoriaId = categoriaId;
            postagem.AutorId = autorId;
            postagem.UrlCapa = capa;

            var revisao = new RevisaoEntity
            {
                Texto = texto,
                Data = DateTime.Now,
                Versao = this.GetUltimaVersaoRevisao(id) + 1,
            };

            postagem.Revisoes.Add(revisao);
            this.db.SaveChanges();

            return postagem;
        }

        internal void Delete(int id)
        {
            var postagem = this.db.Postagems.Find(id);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            this.db.Postagems.Remove(postagem);
        }
    }
}
