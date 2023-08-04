using Domain.Modals.SharedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals
{
    [Table("Users")]
    public class User : Entity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } 
    }
}
