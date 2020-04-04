namespace Blog.Models.Blog.Postagem.Revisao.Classificacao
{
    public class ClassificacaoEntity
    {
        public int Id { get; set; }
        public int PostagemId { get; set; }
        public virtual PostagemEntity Postagem { get; set; }
        public bool Classificacao { get; set; }
    }
}
