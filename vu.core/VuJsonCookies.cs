namespace vu.core;

using Microsoft.AspNetCore.DataProtection;

public class VuJsonCookies
{
    private IDataProtector _protector;

    private string _cake;
    
    private string _protected;

    public string Protect()
    {   
         _protected = _protector.Protect(_cake);
         return _protected;
        
    }

    public string Unprotect()
    {
        return _protector.Unprotect(_protected);
    }


    public VuJsonCookies(IDataProtectionProvider provider, string jsonCake)
    {
        _protector = provider.CreateProtector("vu.core.VuJsonCookies");
        _cake = jsonCake;
    }

    

}