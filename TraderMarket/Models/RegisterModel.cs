using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace TraderMarket.Models
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and numbers are allowed")]
        [Display(Name = "Username")]
        public string username { get; set; }
       
        [Required(ErrorMessage = "Please enter your Password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Minimum length should be 6 characters")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [RegularExpression(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}", ErrorMessage = "Please enter a proper email")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Only letters allowed")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Only letters allowed")]
        [Display(Name = "Surname")]
        public string surname { get; set; }       

        [Required]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "Number is not in the correct format")]
        [Display(Name = "Contact Number")]
        public string contactno { get; set; }

        [Required]
        [Display(Name = "Buyer")]
        public bool buyer { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public bool seller { get; set; }             
                        
        ////[MinLength(34, ErrorMessage = "Length should be 34 digits")]
        //[Display(Name = "IBAN Number")]
        //public string iban { get; set; }

    
	}
}