using System.Collections.Generic;
using System.Linq;
using BusinessObjects;

namespace Repositories
{
    public interface IRoomRepo
    {
        IEnumerable<RoomInformation> GetAllRooms();
        RoomInformation GetRoomById(int roomId);
        void AddRoom(RoomInformation room);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(int roomId);
        IEnumerable<RoomInformation> GetRoomsByType(int roomTypeId);
    }
}
