using System;
using System.Collections.Generic;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepo _roomTypeRepository;

        public RoomTypeService(IRoomTypeRepo roomTypeRepo)
        {
            _roomTypeRepository = roomTypeRepo;
        }

        public List<RoomType> GetAllRoomTypes() => _roomTypeRepository.GetAllRoomTypes();

        public RoomType GetRoomTypeById(int id) => _roomTypeRepository.GetRoomTypeById(id);

        public void AddRoomType(RoomType roomType)
        {
            try
            {
                _roomTypeRepository.AddRoomType(roomType);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Error adding room type: " + ex.Message);
            }
        }

        public void UpdateRoomType(RoomType roomType)
        {
            try
            {
                _roomTypeRepository.UpdateRoomType(roomType);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Error updating room type: " + ex.Message);
            }
        }

        public void DeleteRoomType(int id)
        {
            try
            {
                _roomTypeRepository.DeleteRoomType(id);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Error deleting room type: " + ex.Message);
            }
        }
    }
}
