using Domain.Modals.SharedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals
{
    [Table("Roles")]
    public class Role : Entity
    {
        public string RoleNameEng { get; set; } = string.Empty;
        public string RoleNameBraz { get; set; } = string.Empty;
    }
}
