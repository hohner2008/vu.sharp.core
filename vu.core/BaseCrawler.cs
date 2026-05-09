namespace vu.core;

public class BaseCrawler
{
    private Uri _baseUri;
    
    public Uri Url { 
        get => _baseUri; 
        set  => _baseUri = value; 
    }

    public BaseCrawler(Uri baseUri)
    {
        _baseUri = baseUri;
    }
}