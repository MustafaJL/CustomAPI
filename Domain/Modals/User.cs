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
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        //Add new property 
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        //public long NationalityId { get; set; }
        //[ForeignKey("NationalityId")]
        //public Nationality Nationality { get; set; }





    }
}
