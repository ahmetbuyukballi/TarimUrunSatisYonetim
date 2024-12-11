using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class AppUser :IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HomeAdress { get; set; }
        public string Gender { get; set; }
        public string roles { get; set; }
        [Required]
        [ForeignKey("RolesId")]

        public Guid RoleId { get; set; }
        public AppRoles Role { get; set; }
        public ICollection<Comments> comments { get; set; }
        public ICollection<Orders> Guids { get; set; }
        public ICollection<Products> products { get; set; }
        public ICollection<CardInformation> cardInformation { get; set; }

    }
}
