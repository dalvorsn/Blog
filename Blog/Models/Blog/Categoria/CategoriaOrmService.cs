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
    }
}
