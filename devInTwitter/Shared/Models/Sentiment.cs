using System.ComponentModel.DataAnnotations;

public enum Sentiment{
    [Display(Name="Positivo")]
    Positive = 1,

    [Display(Name="Neutro")]
    Neutral = 2,

    [Display(Name="Negativo")]
    Negative = 3
}