namespace Blog.RequestModels.AdminPostagem
{
    public class AdminPostagemCreate
    {
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string UrlCapa { get; set; }
        public string Texto { get; set; }
    }
}
