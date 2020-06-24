using System.Collections.Generic;

namespace Blog.ViewModels.Admin
{
    public class AdminAutoresListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AdminAutoresAutor> Autores { get; set; }


        public AdminAutoresListarViewModel()
        {
            TituloPagina = "Autores - Administrador";
            Autores = new List<AdminAutoresAutor>();
        }
    }

    public class AdminAutoresAutor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string FotoURL { get; set; }
    }
}