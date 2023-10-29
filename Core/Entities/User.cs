using Core.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }

        [MinLength(3)]
        public string? FirstName { get; set; }

        [MinLength(3)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        [PasswordPropertyText]
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
