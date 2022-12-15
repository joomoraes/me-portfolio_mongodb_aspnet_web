namespace Web.Application.Domain.Security
{
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Cryptography;

    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSsaPssSha256Signature);
        }
    }
}
