using System;
using System.Collections.Generic;
using BusinessObjects;

namespace Services
{
    public interface IRoomService
    {
        IEnumerable<RoomInformation> GetAllRooms();
        RoomInformation GetRoomById(int id);
        void AddRoom(RoomInformation room);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(int id);
        bool IsValidRoomTypeId(int roomTypeId);
        IEnumerable<RoomReport> GenerateReport(DateTime startDate, DateTime endDate);
        IEnumerable<RoomType> GetAllRoomTypes();
        IEnumerable<RoomInformation> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut);
        void BookRoom(int customerId, int roomId, DateTime checkIn, DateTime checkOut);
        IEnumerable<BookingReservation> GetBookingHistory(int customerId);
    }
}
