using Microsoft.Playwright;
using vu.sharp.task;

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

    private IBrowserContext _browserContext;

    public IBrowserContext Context
    {
        get => _browserContext;
    }
    
    private IPage _page;

    public async Task<object> Execute()
    {
        _page = await _browserContext.NewPageAsync();
        await _page.GotoAsync(_baseUri.ToString());
        var status = new VuStatus(_page);
        await _page.WaitForLoadStateAsync();
        var page2 = await _browserContext.NewPageAsync();
        await page2.GotoAsync("https://www.google.com/");
        await page2.WaitForLoadStateAsync();
        return new Task<object>(() => Url.ToString());
    }

    public async Task RunBrowser(BrowserName name)
    {
        if (name == BrowserName.Firefox)
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Firefox.LaunchAsync(new() { Headless = _showBrowser });
            _browserContext = await browser.NewContextAsync();
            
            var result = await Execute();
            _PW = playwright;
            // added close browser
        }

        if (name == BrowserName.Chromium)
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new() { Headless = _showBrowser });
            _browserContext = await browser.NewContextAsync();

            var result = await Execute();
            _PW = playwright;
            // added close browser
        }

        if (name == BrowserName.Webkit)
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Webkit.LaunchAsync(new() { Headless = _showBrowser });
            _browserContext = await browser.NewContextAsync();
            
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