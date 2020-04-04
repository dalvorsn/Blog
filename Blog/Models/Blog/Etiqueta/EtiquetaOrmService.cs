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

        public List<EtiquetaEntity> GetAll()
        {
            return this.db.Etiquetas.ToList();
        }
    }
}
