using System.Collections.Generic;
using System.Linq;
using BusinessObjects;

namespace Services
{
    public interface IRoomService
    {
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
        bool IsValidRoomTypeId(int roomTypeId);
        IEnumerable<RoomReport> GenerateReport(DateTime startDate, DateTime endDate);
        List<RoomType> GetAllRoomTypes();
        List<Room> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut);
        void BookRoom(int customerId, int roomId, DateTime checkIn, DateTime checkOut);
        List<Booking> GetBookingHistory(int customerId);
    }
}
