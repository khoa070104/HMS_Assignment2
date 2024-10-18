using System;
using System.Collections.Generic;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class RoomTypeRepo : IRoomTypeRepo
    {
        private readonly RoomTypeDAO _roomTypeDao = new();

        public List<RoomType> GetAllRoomTypes() => _roomTypeDao.GetAllRoomTypes();

        public RoomType GetRoomTypeById(int roomTypeId) => _roomTypeDao.GetRoomTypeById(roomTypeId);

        public void AddRoomType(RoomType roomType) => _roomTypeDao.AddRoomType(roomType);

        public void UpdateRoomType(RoomType roomType) => _roomTypeDao.UpdateRoomType(roomType);

        public void DeleteRoomType(int roomTypeId) => _roomTypeDao.DeleteRoomType(roomTypeId);
    }
}
