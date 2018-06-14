using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;


namespace CrystalQuartz.Owin
{
     public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public delegate Task<IEnumerable<Claim>> CredentialValidationFunction(string id, string secret);

        public BasicAuthenticationMiddleware(OwinMiddleware next, BasicAuthenticationOptions options)
            : base(next, options)
        {}

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
        {
            return new BasicAuthenticationHandler(Options);
        }
    }
}
