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
