using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Task.Data.Entites
{
    public class User : IdentityUser<int>
    {
        [Required, MaxLength(50)]
        public string FirstName { set; get; }
        public string lasttName { set; get; }
        public string Email { set; get; }

        public int Phone { set; get; }
        public int SSN { set; get; }
    }
}
