namespace Models;
public class Post{
    public int Id {get; set;}
    public string? UsuarioId  {get; set;}
    public string? Conteudo {get; set;}
    public int QuantidadeDePalavras {get; set;}
    public int QuantidadeDeLetras {get; set;}
    public DateTime? CreatedAt {get; set;}

    public Sentiment? Sentimento {get; set;}
    public Emotion? Emocao {get; set;}
    
}