using Microsoft.Playwright;

namespace vu.core;

public class BaseCrawler
{
    public enum BrowserName
    {
        Firefox,
        Chromium,
        Webkit
    };

    private Uri _baseUri;
    
    public Uri Url { 
        get => _baseUri; 
        set  => _baseUri = value; 
    }
    
    private bool _showBrowser;
    public bool ShowBrowser {
        get => _showBrowser;
        set => _showBrowser = value;
    }

    private object? _PW;

    public async Task<object> Execute()
    {
        return new Task<object>(() => Url.ToString());
    }

    public async void RunBrowser(BrowserName name)
    {
        if (name == BrowserName.Firefox)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(
                new BrowserTypeLaunchOptions
            {
                Headless = !_showBrowser
            });
            var result = await Execute();
            _PW = playwright;
            // added close browser
        }

        if (name == BrowserName.Chromium)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = !_showBrowser
                });
            var result = await Execute();
            _PW = playwright;
            // added close browser
        }

        if (name == BrowserName.Webkit)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Webkit.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = !_showBrowser
                });
            var result = await Execute();
            _PW = playwright;
            // added close browser
        }
    }

    public BaseCrawler(Uri baseUri)
    {
        _baseUri = baseUri;
    }
}