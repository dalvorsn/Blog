namespace Blog.ViewModels.Admin
{
    public class AdminCategoriasCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }
        
        public AdminCategoriasCriarViewModel()
        {
            TituloPagina = "Criar nova categoria";
        }
    }
}