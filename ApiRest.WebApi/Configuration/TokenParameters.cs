using ApiRest.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.WebApi.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        public string userName { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get; set; }
    }
}
