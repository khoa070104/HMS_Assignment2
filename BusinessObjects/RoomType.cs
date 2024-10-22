using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class RoomType
    {
        [Key]
        public int RoomTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomTypeName { get; set; }

        [MaxLength(250)]
        public string TypeDescription { get; set; }

        [MaxLength(250)]
        public string TypeNote { get; set; }

        public ICollection<RoomInformation> Rooms { get; set; }
    }
}
