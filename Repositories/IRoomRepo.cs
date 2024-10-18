using System.Collections.Generic;
using BusinessObjects;

namespace Repositories
{
    public interface IRoomRepo
    {
        List<Room> GetAllRooms();
        Room GetRoomById(int roomId);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int roomId);
        List<Room> GetRoomsByType(int roomTypeId);
    }
}
