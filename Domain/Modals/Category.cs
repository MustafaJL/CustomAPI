using Domain.Modals.SharedFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals
{
    public class Category : Entity
    {
        public string CategoryNameEng { get; set; } = string.Empty;
        public string CategoryNameBraz { get; set; } = string.Empty;
    }
}
