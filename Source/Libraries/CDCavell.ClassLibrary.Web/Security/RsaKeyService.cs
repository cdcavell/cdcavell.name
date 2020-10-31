using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;

namespace CDCavell.ClassLibrary.Web.Security
{
    /// <summary>
    /// RsaKeyService to generate signing credentials as outlined by [Ikechi Michael's](https://gist.github.com/mykeels) posting:
    /// &lt;br /&gt;
    /// [For IdentityServer4's AddSigningCredentials in production](https://gist.github.com/mykeels/408a26fb9411aff8fb7506f53c77c57a)
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/01/2020 | Initial build |~ 
    /// </revision>
    public class RsaKeyService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly TimeSpan _timeSpan;

		private string _file
		{
			get
			{
				return Path.Combine(_webHostEnvironment.ContentRootPath, "rsakey.json");
			}
		}

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="environment">IWebHostEnvironment</param>
        /// <param name="timeSpan">TimeSpan</param>
        /// <method>RsaKeyService(IWebHostEnvironment environment, TimeSpan timeSpan)</method>
		public RsaKeyService(IWebHostEnvironment environment, TimeSpan timeSpan)
		{
			_webHostEnvironment = environment;
			_timeSpan = timeSpan;
		}

        /// <summary>
        /// Does key need to be updated
        /// </summary>
        /// <returns>bool</returns>
        /// <method>NeedsUpdate()</method>
        public bool NeedsUpdate()
        {
            if (File.Exists(_file))
            {
                var creationDate = File.GetCreationTime(_file);
                return DateTime.Now.Subtract(creationDate) > _timeSpan;
            }
            return true;
        }

        /// <summary>
        /// Get randome key
        /// </summary>
        /// <returns>RSAParameters</returns>
        /// <method>GetRandomKey()</method>
        public RSAParameters GetRandomKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    return rsa.ExportParameters(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// Generate and save key 
        /// </summary>
        /// <param name="forceUpdate">bool</param>
        /// <returns>RsaKeyService</returns>
        /// <method>GenerateKeyAndSave(bool forceUpdate = false)</method>
        public RsaKeyService GenerateKeyAndSave(bool forceUpdate = false)
        {
            if (forceUpdate || NeedsUpdate())
            {
                var p = GetRandomKey();
                RSAParametersWithPrivate t = new RSAParametersWithPrivate();
                t.SetParameters(p);
                File.WriteAllText(_file, JsonConvert.SerializeObject(t, Formatting.Indented));
            }
            return this;
        }

        /// <summary>
        /// Get key parameters 
        /// </summary>
        /// <returns>RSAParameters</returns>
        /// <method>GetKeyParameters()</method>
        public RSAParameters GetKeyParameters()
        {
            if (!File.Exists(_file)) throw new FileNotFoundException("Check configuration - cannot find auth key file: " + _file);
            var keyParams = JsonConvert.DeserializeObject<RSAParametersWithPrivate>(File.ReadAllText(_file));
            return keyParams.ToRSAParameters();
        }

        /// <summary>
        /// Get key
        /// </summary>
        /// <returns>RsaSecurityKey</returns>
        /// <method>GetKey()</method>
        public RsaSecurityKey GetKey()
        {
            if (NeedsUpdate()) GenerateKeyAndSave();
            var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
            provider.ImportParameters(GetKeyParameters());
            return new RsaSecurityKey(provider);
        }


        /// <summary>
        /// Util class to allow restoring RSA parameters from JSON as the normal
        /// RSA parameters class won't restore private key info.
        /// </summary>
        private class RSAParametersWithPrivate
        {
            public byte[] D { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] Exponent { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }

            public void SetParameters(RSAParameters p)
            {
                D = p.D;
                DP = p.DP;
                DQ = p.DQ;
                Exponent = p.Exponent;
                InverseQ = p.InverseQ;
                Modulus = p.Modulus;
                P = p.P;
                Q = p.Q;
            }
            public RSAParameters ToRSAParameters()
            {
                return new RSAParameters()
                {
                    D = this.D,
                    DP = this.DP,
                    DQ = this.DQ,
                    Exponent = this.Exponent,
                    InverseQ = this.InverseQ,
                    Modulus = this.Modulus,
                    P = this.P,
                    Q = this.Q

                };
            }
        }
    }
}

