using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Task.Data.Entites
{
    public class User : IdentityUser
    {
        public string Full_Name { set; get; }
        public string Adrress { set; get; }
        public int Phone { set; get; }
        public int SSN { set; get; }
    }
}
