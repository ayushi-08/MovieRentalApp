using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentalMVCApplication.Models
{
    public class Min18YrsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if(customer.MembershipTypeId==0 || customer.MembershipTypeId==1)
                            return ValidationResult.Success;
            if (customer.DOB == null)
                return new ValidationResult("Birth Date is Required ");
            var age = DateTime.Today.Year - customer.DOB.Year;
            return (age >= 18) ? ValidationResult.Success
                : new ValidationResult("Age should be greater than 18 years to avil membership");
            
        }
    }
}