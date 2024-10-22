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
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerFullName { get; set; }

        [MaxLength(12)]
        public string Telephone { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public DateTime? CustomerBirthday { get; set; }

        public int CustomerStatus { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public ICollection<BookingReservation> BookingReservations { get; set; }
    }
}
