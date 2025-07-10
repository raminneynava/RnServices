using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class JsonWebToken
    {
        public string? Token { get; set; }
        public long Expires { get; set; }
        public long RefreshToken { get; set; }
    }
}
