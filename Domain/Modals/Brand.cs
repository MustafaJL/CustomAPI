using Domain.Modals.SharedFields;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Modals
{
    public class Brand : Entity
    {
        public string BrandName { get; set; } = string.Empty;

    }
}
