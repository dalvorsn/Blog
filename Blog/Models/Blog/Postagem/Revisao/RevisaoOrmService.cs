using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Blog.Postagem.Revisao
{
    public class RevisaoOrmService
    {
        private readonly Database db;
        public RevisaoOrmService(Database database)
        {
            this.db = database;
        }

        public void AdicionarRevisao(int postagemId, string texto, int versao)
        {
            var postagem = this.db.Postagems.Find(postagemId);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            var revisao = new RevisaoEntity
            {
                Postagem = postagem,
                Texto = texto,
                Data = DateTime.Now,
                Versao = versao,
            };

            this.db.Revisoes.Add(revisao);
            this.db.SaveChanges();
        }
    }
    

}
