using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using BusinessObjects;
namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        private static List<RoomType> roomTypes = new List<RoomType>
        {
            new RoomType { RoomTypeID = 1, RoomTypeName = "Single", TypeDescription = "Single room with one bed", TypeNote = "No extra facilities" },
            new RoomType { RoomTypeID = 2, RoomTypeName = "Double", TypeDescription = "Double room with two beds", TypeNote = "Suitable for couples" },
            
        };

        private static HashSet<int> usedIds = new HashSet<int> { 1, 2 };

        public List<RoomType> GetAllRoomTypes() => roomTypes.OrderBy(rt => rt.RoomTypeID).ToList();

        public RoomType GetRoomTypeById(int roomTypeId) => roomTypes.FirstOrDefault(rt => rt.RoomTypeID == roomTypeId);

        public void AddRoomType(RoomType roomType)
        {
            if (usedIds.Contains(roomType.RoomTypeID))
            {
                throw new ArgumentException("RoomTypeID already exists.");
            }

            int newId = 1;
            while (usedIds.Contains(newId))
            {
                newId++;
            }
            roomType.RoomTypeID = newId;
            usedIds.Add(newId);
            roomTypes.Add(roomType);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            var existingRoomType = GetRoomTypeById(roomType.RoomTypeID);
            if (existingRoomType != null)
            {
                existingRoomType.RoomTypeName = roomType.RoomTypeName;
                existingRoomType.TypeDescription = roomType.TypeDescription;
                existingRoomType.TypeNote = roomType.TypeNote;
            }
            else
            {
                throw new ArgumentException("RoomType not found.");
            }
        }

        public void DeleteRoomType(int roomTypeId)
        {
            var roomType = GetRoomTypeById(roomTypeId);
            if (roomType != null)
            {
                roomTypes.Remove(roomType);
                usedIds.Remove(roomTypeId);
            }
            else
            {
                throw new ArgumentException("RoomType not found.");
            }
        }
    }
}
