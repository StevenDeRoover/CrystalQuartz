using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalQuartz.Owin
{
     public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public BasicAuthenticationMiddleware.CredentialValidationFunction CredentialValidationFunction { get; private set; }
        public string Realm { get; private set; }

        public BasicAuthenticationOptions(string realm, BasicAuthenticationMiddleware.CredentialValidationFunction validationFunction)
            : base("Basic")
        {
            Realm = realm;
            CredentialValidationFunction = validationFunction;
        }
    }
}
