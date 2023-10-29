using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class RegistrationModel
    {
        [Required]

        public string? UserName { get; set; }

        [MinLength(3)]
        public string? FirstName { get; set; }

        [MinLength(3)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [NotMapped]
        public string? RepytPassword { get; set; }

    }
}
