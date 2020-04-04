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

        public List<PostagemEntity> GetAll()
        {
            return db.Postagems
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
    }
}
