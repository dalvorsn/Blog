using Blog.Models.Blog.Etiqueta;
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
        private readonly RevisaoOrmService _revisaoOrmService;
        private readonly EtiquetaOrmService _etiquetaOrmService;
        public PostagemOrmService(Database database, RevisaoOrmService revisaoOrmService, EtiquetaOrmService etiquetaOrmService)
        {
            this.db = database;
            _revisaoOrmService = revisaoOrmService;
            _etiquetaOrmService = etiquetaOrmService;
        }

        public PostagemEntity Get(int id)
        {
            return this.db.Postagems
               .Include(c => c.Categoria)
               .Include(r => r.Revisoes)
               .Include(a => a.Autor)
               .Include(e => e.PostagemEtiquetas)
               .Where(p => p.Id == id)
               .First();
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

        internal PostagemEntity Create(string titulo, int categoriaId, int autorId, string texto, string capa, List<int> etiquetas)
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

            this.AtualizarEtiquetasAsync(postagem.Id, etiquetas).Wait();

            _revisaoOrmService.AdicionarRevisao(postagem.Id, texto, 1);

            return postagem;
        }

        internal PostagemEntity Edit(int id, string titulo, int categoriaId, int autorId, string texto, string capa, List<int> etiquetas)
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

            this.AtualizarEtiquetasAsync(postagem.Id, etiquetas).Wait();

            this.db.SaveChanges();
            _revisaoOrmService.AdicionarRevisao(postagem.Id, texto, this.GetUltimaVersaoRevisao(id) + 1);

            return postagem;
        }

        internal void Delete(int id)
        {
            var postagem = this.db.Postagems.Find(id);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            this.db.Postagems.Remove(postagem);
        }

        public async Task AtualizarEtiquetasAsync(int postagemId, List<int> etiquetas)
        {
            var postagem = this.db.Postagems.Find(postagemId);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            Dictionary<EtiquetaEntity, PostagemEtiquetaEntity> etiquetasSet = new Dictionary<EtiquetaEntity, PostagemEtiquetaEntity>();

            if(postagem.PostagemEtiquetas != null)
            {
                foreach (var pe in postagem.PostagemEtiquetas)
                {
                    etiquetasSet[pe.Etiqueta] = pe;
                }
            }
            
            
            foreach(var etiqueta in this.db.Etiquetas)
            {
                if(etiquetasSet.ContainsKey(etiqueta))
                {
                    if(!etiquetas.Contains(etiqueta.Id))
                    {
                        await _etiquetaOrmService.DesvincularEtiquetaPostagem(etiqueta.Id, postagem.Id);
                    }
                } 
                else
                {
                    if(etiquetas.Contains(etiqueta.Id))
                    {
                        await _etiquetaOrmService.VincularEtiquetaPostagem(etiqueta.Id, postagem.Id);
                    }
                }
            }
        }
    }
}
