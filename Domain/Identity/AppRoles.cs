using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class AppRoles:IdentityRole<int>
    {
      public ICollection<AppUser> AppUsers { get; set; }
    }
}
