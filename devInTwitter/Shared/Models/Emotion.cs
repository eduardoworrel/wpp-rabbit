using System.ComponentModel.DataAnnotations;

public enum Emotion{
    [Display(Name="Felicidade")]
    Happy = 1,

    [Display(Name="Raiva")]
    Angry = 2,

    [Display(Name="Excitado")]
    Excited = 3,

    [Display(Name="Triste")]
    Sad = 4,

    [Display(Name="Medo")]
    Fear = 5,

    [Display(Name="Entediado")]
    Bored = 6
}