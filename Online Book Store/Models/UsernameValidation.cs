using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Book_Store.Models
{
    public class UsernameValidation : ValidationAttribute
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;
            String CurrentUsername = customer.Username;

            foreach (Customer user in database.Customers)
            {
                if (user.Username == CurrentUsername)
                {
                    return new ValidationResult("Username already taken");
                }
            }
            return ValidationResult.Success;
        }
    }
}