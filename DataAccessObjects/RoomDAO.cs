using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using BusinessObjects;

namespace DataAccessLayer
{
    public class RoomDAO
    {
        private static List<Room> rooms = new List<Room>
        {
            new Room { RoomID = 1, RoomNumber = "101", RoomDescription = "Single Room", RoomMaxCapacity = 1, RoomStatus = 1, RoomPricePerDate = 50.0m, RoomTypeID = 1 },
            new Room { RoomID = 2, RoomNumber = "102", RoomDescription = "Double Room", RoomMaxCapacity = 2, RoomStatus = 1, RoomPricePerDate = 80.0m, RoomTypeID = 2 },
        };

        private static HashSet<int> usedIds = new HashSet<int> { 1, 2 };

        public List<Room> GetAllRooms() => rooms.OrderBy(r => r.RoomID).ToList();

        public Room GetRoomById(int roomId) => rooms.FirstOrDefault(r => r.RoomID == roomId);

        public void AddRoom(Room room)
        {
            if (usedIds.Contains(room.RoomID))
            {
                throw new ArgumentException("RoomID already exists.");
            }

            int newId = 1;
            while (usedIds.Contains(newId))
            {
                newId++;
            }
            room.RoomID = newId;
            usedIds.Add(newId);
            rooms.Add(room);
        }

        public void UpdateRoom(Room room)
        {
            var existingRoom = GetRoomById(room.RoomID);
            if (existingRoom != null)
            {
                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.RoomDescription = room.RoomDescription;
                existingRoom.RoomMaxCapacity = room.RoomMaxCapacity;
                existingRoom.RoomStatus = room.RoomStatus;
                existingRoom.RoomPricePerDate = room.RoomPricePerDate;
                existingRoom.RoomTypeID = room.RoomTypeID;
            }
            else
            {
                throw new ArgumentException("Room not found.");
            }
        }

        public void DeleteRoom(int roomId)
        {
            var room = GetRoomById(roomId);
            if (room != null)
            {
                rooms.Remove(room);
                usedIds.Remove(roomId);
            }
            else
            {
                throw new ArgumentException("Room not found.");
            }
        }
    }
}
