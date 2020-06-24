namespace Blog.ViewModels.Admin
{
    public class AdminAutoresCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }
        
        public AdminAutoresCriarViewModel()
        {
            TituloPagina = "Criar novo autor";
        }
    }
}