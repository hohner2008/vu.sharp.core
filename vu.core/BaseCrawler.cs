namespace vu.core;

public class BaseCrawler
{
    private Uri _baseUri;
    
    public string Url { 
        get => _baseUri.ToString(); 
        set  => _baseUri = new Uri(value); 
    }

    public BaseCrawler(Uri baseUri)
    {
        _baseUri = baseUri;
    }
}