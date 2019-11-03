using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "ProductName is reguired")]
        [MaxLength(100, ErrorMessage = "ProductName cannot exceed 100 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "ProductCount is required")]
        public int ProductCount { get; set; }

        [Required(ErrorMessage = "PhotoPath is required")]
        public IFormFile Photo { get; set; }
    }
}
