using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BaseResult
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public User? UserInfo { get; set; }
    }
}
