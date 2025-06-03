using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectAPI.BusinessLogicLayer.DTOs.AuthDTOs
{
    public class RegisterDTO
    {

        [Required(ErrorMessage = "Please enter a valid user name.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a strong password.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please select a role.")]
        public string Role { get; set; }


    }
}