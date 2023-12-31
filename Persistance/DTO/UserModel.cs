﻿using Domain.Modals;

namespace Persistance.DTO
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }

    }
}
