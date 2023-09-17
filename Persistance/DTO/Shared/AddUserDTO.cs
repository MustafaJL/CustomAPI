using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DTO.Shared
{
    public class AddUserDTO
    {
        public long Id { get; set; }
        public string? firstName { get; set;}
        public string? lastName { get; set;}
        public string? Email { get; set;}
        public long roleId { get; set;}
        public string? phoneNumber {get; set;}
        public string? Address { get; set;}
        public DateTime dateOfBirth { get; set;}
        public string? Gender { get; set;}
        public bool isActive { get; set;}
    }
}
