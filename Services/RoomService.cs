using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepo _roomRepository;
        private readonly IRoomTypeRepo _roomTypeRepository;
        private readonly IBookingRepo _bookingRepository;
        private readonly ICustomerRepo _customerRepository;

        public RoomService(IRoomRepo roomRepo, IRoomTypeRepo roomTypeRepo, IBookingRepo bookingRepo, ICustomerRepo customerRepo)
        {
            _roomRepository = roomRepo;
            _roomTypeRepository = roomTypeRepo;
            _bookingRepository = bookingRepo;
            _customerRepository = customerRepo;
        }

        public List<Room> GetAllRooms() => _roomRepository.GetAllRooms();

        public Room GetRoomById(int id) => _roomRepository.GetRoomById(id);

        public void AddRoom(Room room)
        {
            if (!IsValidRoomTypeId(room.RoomTypeID))
            {
                throw new ArgumentException("Invalid RoomTypeID. The RoomTypeID must exist in the RoomType collection.");
            }
            try
            {
                _roomRepository.AddRoom(room);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Error adding room: " + ex.Message);
            }
        }

        public void UpdateRoom(Room room)
        {
            if (!IsValidRoomTypeId(room.RoomTypeID))
            {
                throw new ArgumentException("Invalid RoomTypeID. The RoomTypeID must exist in the RoomType collection.");
            }
            try
            {
                _roomRepository.UpdateRoom(room);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Error updating room: " + ex.Message);
            }
        }

        public void DeleteRoom(int id)
        {
            try
            {
                _roomRepository.DeleteRoom(id);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Error deleting room: " + ex.Message);
            }
        }

        public bool IsValidRoomTypeId(int roomTypeId)
        {
            return _roomTypeRepository.GetRoomTypeById(roomTypeId) != null;
        }

        public IEnumerable<RoomReport> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var allBookings = _bookingRepository.GetAllBookings()
                .Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate)
                .ToList();

            var roomReports = _roomRepository.GetAllRooms()
                .Select(room => new RoomReport
                {
                    RoomNumber = room.RoomNumber,
                    TotalBookings = 0,
                    TotalRevenue = 0,
                    CustomerNames = new List<string>()
                })
                .ToList();

            foreach (var booking in allBookings)
            {
                var report = roomReports.First(r => r.RoomNumber == booking.RoomNumber);
                report.TotalBookings++;
                report.TotalRevenue += CalculateTotalPrice(booking);
                
                var customer = _customerRepository.GetCustomerById(booking.CustomerID);
                if (customer != null && !report.CustomerNames.Contains(customer.CustomerFullName))
                {
                    report.CustomerNames.Add(customer.CustomerFullName);
                }
            }

            return roomReports.OrderByDescending(r => r.TotalRevenue);
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _roomTypeRepository.GetAllRoomTypes();
        }

        public List<Room> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            var allRooms = _roomRepository.GetRoomsByType(roomTypeId);
            var bookedRooms = _bookingRepository.GetBookedRooms(checkIn, checkOut);

            return allRooms.Where(room => !bookedRooms.Contains(room.RoomID)).ToList();
        }

        public void BookRoom(int customerId, int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null)
            {
                throw new ArgumentException("Invalid room ID");
            }

            var booking = new Booking
            {
                CustomerID = customerId,
                RoomID = roomId,
                RoomNumber = room.RoomNumber,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                TotalPrice = CalculateTotalPrice(room, checkIn, checkOut)
            };

            _bookingRepository.AddBooking(booking);
        }

        public List<Booking> GetBookingHistory(int customerId)
        {
            return _bookingRepository.GetBookingsByCustomer(customerId);
        }

        private decimal CalculateTotalPrice(Room room, DateTime checkIn, DateTime checkOut)
        {
            int numberOfDays = (int)(checkOut - checkIn).TotalDays;
            return room.RoomPricePerDate * numberOfDays;
        }

        private decimal CalculateTotalPrice(Booking booking)
        {
            var room = _roomRepository.GetRoomById(booking.RoomID);
            int numberOfDays = (int)(booking.CheckOutDate - booking.CheckInDate).TotalDays;
            return room.RoomPricePerDate * numberOfDays;
        }
    }
}
