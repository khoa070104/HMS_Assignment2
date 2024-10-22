using System;
using System.Collections.Generic;
using BusinessObjects;

namespace Services
{
    public interface IBookingService
    {
        void AddBooking(BookingReservation booking);
        IEnumerable<BookingReservation> GetBookingsByCustomer(int customerId);
        IEnumerable<int> GetBookedRooms(DateTime checkIn, DateTime checkOut);
        IEnumerable<BookingReservation> GetAllBookings();
    }
}
