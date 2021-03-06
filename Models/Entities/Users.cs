// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OrderForMeProject.Models.Entities
{
    public partial class Users
    {
        public Users()
        {
            Logger = new HashSet<Logger>();
            Orders = new HashSet<Orders>();
        }

        public int UsersId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public float Balance { get; set; }
        public DateTime CreateOn { get; set; }
        public bool? IsActive { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Logger> Logger { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}