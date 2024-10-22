using System;
using System.Collections.Generic;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepo _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepo();
        }

        public RoomTypeService(IRoomTypeRepo roomTypeRepo)
        {
            _roomTypeRepository = roomTypeRepo;
        }

        public IEnumerable<RoomType> GetAllRoomTypes() => _roomTypeRepository.GetAllRoomTypes();

        public RoomType GetRoomTypeById(int id) => _roomTypeRepository.GetRoomTypeById(id);

        public void AddRoomType(RoomType roomType) => _roomTypeRepository.AddRoomType(roomType);

        public void UpdateRoomType(RoomType roomType)
        {
            var existingRoomType = _roomTypeRepository.GetRoomTypeById(roomType.RoomTypeID);
            if (existingRoomType != null)
            {
                _roomTypeRepository.UpdateRoomType(roomType);
            }
            else
            {
                throw new ArgumentException("RoomType not found");
            }
        }

        public void DeleteRoomType(int id) => _roomTypeRepository.DeleteRoomType(id);
    }
}
