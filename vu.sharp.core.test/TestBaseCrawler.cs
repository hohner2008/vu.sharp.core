using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Playwright;
using vu.core;
using Xunit;

namespace vu.sharp.core.test;


public class TestBaseCrawler
{
    private readonly ITestOutputHelper _output;

    String TestF(IPage page)
    { 
        Console.WriteLine(page);
        return "test";
    }


    [Fact]
    public void TestUrlProperty()
    {   
        var testUri = "http://www.google.com/";
        var crawler = new BaseCrawler(new Uri(testUri));
        _output.WriteLine("Test read uri property: {0}", crawler.Url);
        Assert.Equal(testUri, crawler.Url.AbsoluteUri);
        var newUri = "http://www.qt.io/";
        crawler.Url = new Uri(newUri);
        _output.WriteLine("Test setup uri property: {0}", crawler.Url);
        Assert.Equal(new Uri(newUri), crawler.Url);
    }

    [Fact]
    public async Task TestRunBrowser()
    {
        var testUri = "http://www.qt.io";
        var crawler = new BaseCrawler(new Uri(testUri));
        await crawler.RunBrowser(BaseCrawler.BrowserName.Firefox);
        Assert.Equal("firefox", crawler.Context.Browser?.BrowserType.Name);
        
        var chromeCrawler = new BaseCrawler(new Uri(testUri));
        await chromeCrawler.RunBrowser(BaseCrawler.BrowserName.Chromium);
        Assert.Equal("chromium", chromeCrawler.Context.Browser?.BrowserType.Name);


        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var webkitCrawler = new BaseCrawler(new Uri(testUri));
            await webkitCrawler.RunBrowser(BaseCrawler.BrowserName.Webkit);
            Assert.Equal("webkit", webkitCrawler.Context.Browser?.BrowserType.Name);
        }
    }

    [Fact]
    public async Task TestMainFunctionPropertyIsNull()
    {
        var testUri = "http://www.qt.io";
        var crawler = new BaseCrawler(new Uri(testUri));
        await crawler.RunBrowser(BaseCrawler.BrowserName.Firefox);

        var field = crawler.GetType().GetField("_mainFunction",BindingFlags.Instance | BindingFlags.NonPublic);
        var obj = field?.GetValue(crawler);
        _output.WriteLine("Test _mainFunction property is not setup(NULL): ");
        Assert.Null(obj);
    }

    [Fact]
    public async Task TestSetMainFunction()
    {
        var testUri = "http://www.qt.io";
        var crawler = new BaseCrawler(new Uri(testUri));
        crawler.SetMainFunction(TestF);
        var field = crawler.GetType().GetField("_mainFunction",BindingFlags.Instance | BindingFlags.NonPublic);
        var obj = field?.GetValue(crawler);
        _output.WriteLine("Test _mainFunction property is not NULL ");
        Assert.NotNull(obj);
        vu.core.F? f = obj as vu.core.F;
        string name = f.Method.Name;
        _output.WriteLine("Test _mainFunction property is TestF ");
        Assert.Equal("TestF", name);

    }
    
    [Fact]
    public async Task TestSetMainFunctionNullArgument()
    {
        try
        {
            var testUri = "http://www.qt.io";
            var crawler = new BaseCrawler(new Uri(testUri));
            crawler.SetMainFunction(null);
        }
        catch (Exception e)
        {
            _output.WriteLine("Test SetMainFunction null argument throw exception as System.NullReferenceException ");
            Assert.Equal(typeof(System.NullReferenceException), e.GetType());
        }
    }
    
    public TestBaseCrawler(ITestOutputHelper output)
    {
        _output = output;
    }
}