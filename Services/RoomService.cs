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

        public RoomService()
        {
            _roomRepository = new RoomRepo();
            _roomTypeRepository = new RoomTypeRepo();
            _bookingRepository = new BookingRepo();
            _customerRepository = new CustomerRepo();
        }

        public RoomService(IRoomRepo roomRepo, IRoomTypeRepo roomTypeRepo, IBookingRepo bookingRepo, ICustomerRepo customerRepo)
        {
            _roomRepository = roomRepo;
            _roomTypeRepository = roomTypeRepo;
            _bookingRepository = bookingRepo;
            _customerRepository = customerRepo;
        }

        public IEnumerable<RoomInformation> GetAllRooms() => _roomRepository.GetAllRooms();

        public RoomInformation GetRoomById(int id) => _roomRepository.GetRoomById(id);

        public void AddRoom(RoomInformation room)
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

        public void UpdateRoom(RoomInformation room)
        {
            if (!IsValidRoomTypeId(room.RoomTypeID))
            {
                throw new ArgumentException("Invalid RoomTypeID. The RoomTypeID must exist in the RoomType collection.");
            }
            try
            {
                // Chỉ gửi các thuộc tính cần thiết để cập nhật
                var roomToUpdate = new RoomInformation
                {
                    RoomID = room.RoomID,
                    RoomNumber = room.RoomNumber,
                    RoomDescription = room.RoomDescription,
                    RoomMaxCapacity = room.RoomMaxCapacity,
                    RoomPricePerDate = room.RoomPricePerDate,
                    RoomTypeID = room.RoomTypeID
                };
                _roomRepository.UpdateRoom(roomToUpdate);
            }
            catch (Exception ex)
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
                .Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate)
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
                foreach (var detail in booking.BookingDetails)
                {
                    var report = roomReports.First(r => r.RoomNumber == detail.Room.RoomNumber);
                    report.TotalBookings++;
                    report.TotalRevenue += detail.ActualPrice;
                    
                    var customer = _customerRepository.GetCustomerById(booking.CustomerID);
                    if (customer != null && !report.CustomerNames.Contains(customer.CustomerFullName))
                    {
                        report.CustomerNames.Add(customer.CustomerFullName);
                    }
                }
            }

            return roomReports.OrderByDescending(r => r.TotalRevenue);
        }

        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            return _roomTypeRepository.GetAllRoomTypes();
        }

        public IEnumerable<RoomInformation> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            var allRooms = _roomRepository.GetRoomsByType(roomTypeId);
            var bookedRooms = _bookingRepository.GetBookedRooms(checkIn, checkOut);

            return allRooms.Where(room => !bookedRooms.Contains(room.RoomID));
        }

        public void BookRoom(int customerId, int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null)
            {
                throw new ArgumentException("Invalid room ID");
            }

            var booking = new BookingReservation
            {
                CustomerID = customerId,
                BookingDate = DateTime.Now,
                TotalPrice = CalculateTotalPrice(room, checkIn, checkOut),
                BookingStatus = 1 // Assuming 1 is for Active
            };

            var bookingDetail = new BookingDetail
            {
                RoomID = roomId,
                StartDate = checkIn,
                EndDate = checkOut,
                ActualPrice = CalculateTotalPrice(room, checkIn, checkOut)
            };

            booking.BookingDetails = new List<BookingDetail> { bookingDetail };

            _bookingRepository.AddBooking(booking);
        }

        public IEnumerable<BookingReservation> GetBookingHistory(int customerId)
        {
            return _bookingRepository.GetBookingsByCustomer(customerId);
        }

        private decimal CalculateTotalPrice(RoomInformation room, DateTime checkIn, DateTime checkOut)
        {
            int numberOfDays = (int)(checkOut - checkIn).TotalDays;
            return room.RoomPricePerDate * numberOfDays;
        }
    }
}
