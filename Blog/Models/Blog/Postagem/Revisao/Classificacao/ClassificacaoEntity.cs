namespace Blog.Models.Blog.Postagem.Revisao.Classificacao
{
    public class ClassificacaoEntity
    {
        public int Id { get; set; }

        public int RevisaoId { get; set; }
        public virtual RevisaoEntity Revisao { get; set; }
        public bool Classificacao { get; set; }
    }
}
