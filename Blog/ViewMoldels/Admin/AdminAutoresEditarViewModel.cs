namespace Blog.ViewModels.Admin
{
    public class AdminAutoresEditarViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string FotoURL { get; set; }


        public string Erro { get; set; }
        
        
        public AdminAutoresEditarViewModel()
        {
            TituloPagina = "Editar Autor: ";
        }
    }
}