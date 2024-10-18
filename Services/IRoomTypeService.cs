using System.Collections.Generic;
using BusinessObjects;

namespace Services
{
    public interface IRoomTypeService
    {
        List<RoomType> GetAllRoomTypes();
        RoomType GetRoomTypeById(int id);
        void AddRoomType(RoomType roomType);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(int id);
    }
}
