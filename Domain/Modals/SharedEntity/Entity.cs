using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals.SharedFields
{
    public abstract class Entity
    {
        [Key]
        public long Id { get; set; }
    }
}
