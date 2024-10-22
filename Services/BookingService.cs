using System;
using System.Collections.Generic;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;

        public BookingService(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public void AddBooking(BookingReservation booking)
        {
            _bookingRepo.AddBooking(booking);
        }

        public IEnumerable<BookingReservation> GetBookingsByCustomer(int customerId)
        {
            return _bookingRepo.GetBookingsByCustomer(customerId);
        }

        public IEnumerable<int> GetBookedRooms(DateTime checkIn, DateTime checkOut)
        {
            return _bookingRepo.GetBookedRooms(checkIn, checkOut);
        }

        public IEnumerable<BookingReservation> GetAllBookings()
        {
            return _bookingRepo.GetAllBookings();
        }
    }
}
