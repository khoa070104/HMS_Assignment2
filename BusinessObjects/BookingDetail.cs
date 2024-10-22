using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class BookingDetail
    {
        [Key]
        public int BookingDetailID { get; set; }

        public int BookingReservationID { get; set; }

        [ForeignKey("BookingReservationID")]
        public BookingReservation BookingReservation { get; set; }

        public int RoomID { get; set; }

        [ForeignKey("RoomID")]
        public RoomInformation Room { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ActualPrice { get; set; }
    }
}
