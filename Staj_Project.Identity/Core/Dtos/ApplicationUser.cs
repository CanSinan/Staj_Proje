using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staj_Project.Identity.Core.Dtos
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

    }
}
