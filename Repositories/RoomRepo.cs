using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RoomRepo : IRoomRepo
    {
        private HmsDbContext _context;

        public RoomRepo()
        {
            _context = new HmsDbContext();
        }

        public IEnumerable<RoomInformation> GetAllRooms()
        {
            return _context.RoomInformations.Include(r => r.RoomType).ToList();
        }

        public RoomInformation GetRoomById(int roomId)
        {
            return _context.RoomInformations.Include(r => r.RoomType).FirstOrDefault(r => r.RoomID == roomId);
        }

        public void AddRoom(RoomInformation room)
        {
            _context.RoomInformations.Add(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(RoomInformation room)
        {
            var existingRoom = _context.RoomInformations
                .Include(r => r.RoomType)
                .FirstOrDefault(r => r.RoomID == room.RoomID);

            if (existingRoom != null)
            {
                // Cập nhật các thuộc tính của phòng
                _context.Entry(existingRoom).CurrentValues.SetValues(room);

                // Cập nhật RoomType nếu cần
                if (existingRoom.RoomTypeID != room.RoomTypeID)
                {
                    var newRoomType = _context.RoomTypes.Find(room.RoomTypeID);
                    if (newRoomType != null)
                    {
                        existingRoom.RoomType = newRoomType;
                    }
                }

                _context.SaveChanges();
            }
        }

        public void DeleteRoom(int roomId)
        {
            var room = _context.RoomInformations.Find(roomId);
            if (room != null)
            {
                _context.RoomInformations.Remove(room);
                _context.SaveChanges();
            }
        }

        public IEnumerable<RoomInformation> GetRoomsByType(int roomTypeId)
        {
            return _context.RoomInformations.Where(r => r.RoomTypeID == roomTypeId).ToList();
        }
    }
}
