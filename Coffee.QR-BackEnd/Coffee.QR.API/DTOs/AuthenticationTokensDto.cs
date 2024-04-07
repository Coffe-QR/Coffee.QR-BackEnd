using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class AuthenticationTokensDto
    {
        public long Id { get; set; }
        public string AccessToken { get; set; }
    }
}
