using vu.core;
using Xunit;

namespace vu.sharp.core.test;

public class TestBaseCrawler
{
    private readonly ITestOutputHelper _output;
    
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
    }
    
    public TestBaseCrawler(ITestOutputHelper output)
    {
        _output = output;
    }
}