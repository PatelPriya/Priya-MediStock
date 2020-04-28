using DAL.Domains;
using System;
using System.ComponentModel.DataAnnotations;

namespace MediStockWeb.Areas.Admin.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Password = new Password();
        }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string Passwordstr { get; set; }
        public Password Password { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
