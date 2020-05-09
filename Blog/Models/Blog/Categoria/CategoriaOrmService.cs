using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Models.Blog.Categoria
{
    public class CategoriaOrmService
    {
        public readonly Database db;

        public CategoriaOrmService(Database db)
        {
            this.db = db;
        }

        public List<CategoriaEntity> GetAll()
        {
            return this.db.Categorias.ToList();
        }

        public CategoriaEntity GetById(int id)
        {
            return this.db.Categorias.Find(id);
        }

        public CategoriaEntity Create(string nome)
        {
            var categoria = new CategoriaEntity { Nome = nome };
            this.db.Categorias.Add(categoria);
            this.db.SaveChanges();

            return categoria;
        }

        public CategoriaEntity Edit(int id, string nome)
        {
            var categoria = this.GetById(id);
            if (categoria == null)
                throw new Exception("Categoria não encontrada!");

            categoria.Nome = nome;
            this.db.SaveChanges();

            return categoria;
        }

        public CategoriaEntity Delete(int id)
        {
            var categoria = this.GetById(id);
            if (categoria == null)
                throw new Exception("Categoria não encontrada!");

            this.db.Categorias.Remove(categoria);
            this.db.SaveChanges();

            return categoria;
        }
    }
}
