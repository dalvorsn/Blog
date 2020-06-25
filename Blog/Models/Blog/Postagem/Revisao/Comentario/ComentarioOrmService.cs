using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Blog.Postagem.Revisao.Comentario
{
    public class ComentarioOrmService
    {
        private readonly Database db;
        public ComentarioOrmService(Database database)
        {
            this.db = database;
        }


    }
}
