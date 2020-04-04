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
    }
}
