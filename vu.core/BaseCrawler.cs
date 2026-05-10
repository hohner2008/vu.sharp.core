namespace vu.core;

public class BaseCrawler
{
    public enum BrowserName
    {
        FIREFOX,
        CHROMIUM,
        WEBKIT =3
    };

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