using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [MaxLength(50)]
        public string CustomerFullName { get; set; }
        [MaxLength(12)]
        public string Telephone { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public DateTime? CustomerBirthday { get; set; }
        public int CustomerStatus { get; set; } 
        [MaxLength(50)]
        public string Password { get; set; }
    }
}