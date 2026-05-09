using vu.core;
using Xunit;

namespace vu.sharp.core.test;

public class TestBaseCrawler
{
    [Fact]
    public void TestUrlProperty()
    {   
        var testUri = "http://www.google.com/";
        var crawler = new BaseCrawler(new Uri(testUri));
        Assert.Equal(testUri, crawler.Url.AbsoluteUri);
        var newUri = "http://www.qt.io/";
        crawler.Url = new Uri(newUri);
        Assert.Equal(new Uri(newUri), crawler.Url);
    }
}