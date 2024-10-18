using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class RoomRepo : IRoomRepo
    {
        private readonly RoomDAO _roomDao = new();

        public List<Room> GetAllRooms() => _roomDao.GetAllRooms();

        public Room GetRoomById(int roomId) => _roomDao.GetRoomById(roomId);

        public void AddRoom(Room room) => _roomDao.AddRoom(room);

        public void UpdateRoom(Room room) => _roomDao.UpdateRoom(room);

        public void DeleteRoom(int roomId) => _roomDao.DeleteRoom(roomId);

        public List<Room> GetRoomsByType(int roomTypeId)
        {
            return _roomDao.GetAllRooms().Where(r => r.RoomTypeID == roomTypeId).ToList();
        }
    }
}
