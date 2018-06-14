using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrystalQuartz.WebFramework.HttpAbstractions
{
    public class NotAuthorizedResponse : Response
    {
        public NotAuthorizedResponse()
            : base("", (int)HttpStatusCode.Unauthorized, null)
        { }
    }
}
