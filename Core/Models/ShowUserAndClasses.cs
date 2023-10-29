using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ShowUserAndClasses
    {
        public List<User>? Users { get; set; }
        public List<Class>? Classes { get; set; }

    }
}
