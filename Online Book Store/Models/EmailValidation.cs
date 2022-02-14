using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Book_Store.Models
{
    public class EmailValidation : ValidationAttribute
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;
            String CurrentEmailId = customer.Email;

            foreach (Customer user in database.Customers)
            {
                if (user.Email == CurrentEmailId)
                {
                    return new ValidationResult("Account already exist with this email id!");
                }
            }
            return ValidationResult.Success;
        }
    }
}