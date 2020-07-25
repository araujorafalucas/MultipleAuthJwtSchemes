using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleAuthSchemes.Models
{
    public class ClientToken
    {
        public string Token { get; set; }
        public DateTime DateExpiration { get; set; }
    }
}
