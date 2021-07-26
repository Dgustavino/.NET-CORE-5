using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Abstraction
{
    public interface ITokenParameters
    {
        string userName { get; set; }

        string PasswordHash { get; set; }

        string Id { get; set; }
    }
}
