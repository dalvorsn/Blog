using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Models.Blog.Autor
{
    public class AutorOrmService
    {
        private readonly Database db;

        public AutorOrmService(Database db)
        {
            this.db = db;
        }

        public List<AutorEntity> GetAll()
        {
            return this.db.Autores.ToList();
        }

        public AutorEntity Create(string nome, string fotoURL)
        {
            var autor = new AutorEntity { Nome = nome, FotoURL = fotoURL };
            this.db.Autores.Add(autor);
            this.db.SaveChanges();

            return autor;
        }

        public AutorEntity Edit(int id, string nome, string fotoURL)
        {
            var autor = this.db.Autores.Find(id);
            if (autor == null)
                throw new Exception("Autor não encontrada!");

            autor.Nome = nome;
            autor.FotoURL = fotoURL;
            this.db.SaveChanges();

            return autor;
        }

        public AutorEntity Delete(int id)
        {
            var autor = this.db.Autores.Find(id);
            if (autor == null)
                throw new Exception("Autor não encontrada!");

            this.db.Autores.Remove(autor);
            this.db.SaveChanges();

            return autor;
        }
    }
}
