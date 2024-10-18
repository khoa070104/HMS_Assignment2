using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class Room
    {
        public int RoomID { get; set; }
        [MaxLength(50)]
        public string RoomNumber { get; set; }
        [MaxLength(220)]
        public string RoomDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomStatus { get; set; } 
        public decimal RoomPricePerDate { get; set; }
        public int RoomTypeID { get; set; }
    }
}
