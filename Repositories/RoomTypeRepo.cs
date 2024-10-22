using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RoomTypeRepo : IRoomTypeRepo
    {
        private readonly HmsDbContext _context;

        public RoomTypeRepo(HmsDbContext context)
        {
            _context = context;
        }

        public RoomTypeRepo()
        {
            _context = new HmsDbContext();
        }

        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            return _context.RoomTypes.ToList();
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            return _context.RoomTypes.Find(roomTypeId);
        }

        public void AddRoomType(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            _context.SaveChanges();
        }

        public void UpdateRoomType(RoomType roomType)
        {
            var existingRoomType = _context.RoomTypes.Find(roomType.RoomTypeID);
            if (existingRoomType != null)
            {
                _context.Entry(existingRoomType).CurrentValues.SetValues(roomType);
                _context.SaveChanges();
            }
        }

        public void DeleteRoomType(int roomTypeId)
        {
            var roomType = _context.RoomTypes.Find(roomTypeId);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                _context.SaveChanges();
            }
        }
    }
}
