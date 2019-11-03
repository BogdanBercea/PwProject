using Microsoft.AspNetCore.Http;
using PW_Proj.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.ViewModels
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "UserName is reguired")]
        [MaxLength(50, ErrorMessage = "UserName cannot exceed 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 6 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string CofirmPassword { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public GenderSelector? Gender { get; set; }

        public IFormFile Photo { get; set; }
    }
}
