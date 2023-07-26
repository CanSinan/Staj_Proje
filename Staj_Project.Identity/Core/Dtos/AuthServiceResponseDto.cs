using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staj_Project.Identity.Core.Dtos
{
    public class AuthServiceResponseDto
    {
        public object Data { get; set; }
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
       
    }
}
