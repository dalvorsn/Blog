namespace Blog.ViewModels.Home
{
    public class HomeDetailsViewModel : ViewModelAreaComum
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Capa { get; set; }
        public string Texto { get; set; }
        public HomeDetailsViewModel()
        {
            TituloPagina = "";
        }
    }


}
