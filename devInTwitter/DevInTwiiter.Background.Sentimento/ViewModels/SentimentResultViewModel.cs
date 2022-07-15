namespace ViewModels;
public class SentimentResultViewModel{
    public SentimentViewModel? Sentiment { get;set;}
}

public class SentimentViewModel{
    public string? Negative { get;set;}
    public string? Neutral { get;set;}
    public string? Positive { get;set;}
}