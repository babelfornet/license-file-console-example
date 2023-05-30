using System;
using Babel.Licensing;

class Program
{
    /// <summary>
    /// Public key used to validate the license signature.
    /// </summary>
    static string PublicKey => "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDE1VRiIdr6fiVZKve7NVgjIvGdRiRx0Mjjm+Yzf6tLbzFnxLs0fat5EoRcubxx0QQQDfydsJBE/fc7cwRWSrE2xK6X4Eb4W8O47pCMjqvTQZfDqQywEZJrLlxpp9hlKz6FDYX4SagrjmP1gdw8olo+n+IBz8ubkNxRhvycikxuDQIDAQAB";
    
    static Program()
    {
        // Register the file license provider for the Program class
        // that will be used by the BabelLicenseManager to locate the license file.
        // The license file should be named as the assembly name with .lic extension.        
        BabelFileLicenseProvider provider = new BabelFileLicenseProvider();

        // Sets the public key used to validate the license signature
        provider.SignatureProvider = RSASignature.FromKeys(PublicKey);

        // Uncomment the following line to customize the license file path
        // provider.LicenseFilePath = "ClientFile.xml";

        // Register the provider for the Program class
        BabelLicenseManager.RegisterLicenseProvider(typeof(Program), provider);
    }

    static void Main(string[] args)
    {
        var program = new Program();

        try
        {
            var license = program.ValidateLicense();

            Console.WriteLine($"License {license.Id} registered to {license.Licensee?.Company}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    public ILicense ValidateLicense()
    {
        // Uses the registered file license provider for Program class
        return BabelLicenseManager.Validate(typeof(Program), this);
    }
}