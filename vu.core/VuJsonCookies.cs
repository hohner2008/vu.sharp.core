namespace vu.core;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Playwright;
using System.Text.Json;

public class VuJsonCookies
{
    private IDataProtector _protector;

    private string _cake;
    

    public VuJsonCookies(IDataProtectionProvider provider, string jsonCake)
    {
        _protector = provider.CreateProtector("vu.core.VuCookies");
        _cake = jsonCake;
    }


}