using Blog.Models.Blog.Postagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly Database db;

        public EtiquetaOrmService(Database db)
        {
            this.db = db;
        }


        public void VincularEtiquetaPostagem(int id, int postagemId)
        {
            var etiqueta = this.db.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            var postagem = this.db.Postagems.Find(postagemId);
            if (postagem == null)
                throw new Exception("Postagem não encontrada!");

            this.db.PostagemEtiquetas.Add(new PostagemEtiquetaEntity
            {
                Postagem = postagem,
                Etiqueta = etiqueta
            });

            db.SaveChangesAsync();
        }

        public void DesvincularEtiquetaPostagem(int id, int postagemId)
        {
            var etiqueta = this.db.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            var postagem = this.db.Postagems.Find(postagemId);
            if (postagem == null)
                throw new Exception("Postagem não encontrada!");

            var entity = this.db.PostagemEtiquetas.Where(ep => ep.EtiquetaId == id && ep.PostagemId == postagemId).First();
            if (entity == null)
                throw new Exception("Vinculo não não encontrada!");

            this.db.PostagemEtiquetas.Remove(entity);
            this.db.SaveChanges();
        }

        public EtiquetaEntity Get(int id)
        {
            return this.db.Etiquetas.Find(id);
        }

        public List<EtiquetaEntity> GetAll()
        {
            return this.db.Etiquetas.ToList();
        }

        public EtiquetaEntity Create(string nome, string cor)
        {
            var etiqueta = new EtiquetaEntity { Nome = nome, Cor = cor };
            this.db.Etiquetas.Add(etiqueta);
            this.db.SaveChanges();

            return etiqueta;
        }

        public EtiquetaEntity Edit(int id, string nome, string cor)
        {
            var etiqueta = this.db.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            etiqueta.Nome = nome;
            etiqueta.Cor = cor;
            this.db.SaveChanges();

            return etiqueta;
        }

        public EtiquetaEntity Delete(int id)
        {
            var etiqueta = this.db.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            this.db.Etiquetas.Remove(etiqueta);
            this.db.SaveChanges();

            return etiqueta;
        }
    }
}
