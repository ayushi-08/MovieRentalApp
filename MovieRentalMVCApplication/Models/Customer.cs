using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentalMVCApplication.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Customer Name is Mandatory")]
        [StringLength(50)]
        [Display(Name="Customer Name")]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        [Display(Name = "Membership Type")]
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        [Min18YrsIfAMember]
        [Display(Name="Date of Birth")]
        public DateTime DOB { get;set; }
    }
}