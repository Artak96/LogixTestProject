using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ClassModel
    {
        [NotMapped]
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
    }
}
