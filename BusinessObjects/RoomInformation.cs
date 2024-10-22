using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class RoomInformation
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomNumber { get; set; }

        [MaxLength(220)]
        public string RoomDescription { get; set; }

        public int RoomMaxCapacity { get; set; }

        public int RoomStatus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RoomPricePerDate { get; set; }

        public int RoomTypeID { get; set; }

        [ForeignKey("RoomTypeID")]
        public RoomType RoomType { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
