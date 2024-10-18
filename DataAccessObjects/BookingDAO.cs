using System;
using System.Collections.Generic;
using System.Linq;

public class BookingDAO
{
    private static List<Booking> bookings = new List<Booking>();

    public void AddBooking(Booking booking)
    {
        booking.BookingID = bookings.Count + 1;
        bookings.Add(booking);
    }

    public List<Booking> GetBookingsByCustomer(int customerId)
    {
        return bookings.Where(b => b.CustomerID == customerId).ToList();
    }

    public List<int> GetBookedRooms(DateTime checkIn, DateTime checkOut)
    {
        return bookings.Where(b => 
            (b.CheckInDate <= checkIn && checkIn < b.CheckOutDate) ||
            (b.CheckInDate < checkOut && checkOut <= b.CheckOutDate) ||
            (checkIn <= b.CheckInDate && b.CheckOutDate <= checkOut))
            .Select(b => b.RoomID)
            .Distinct()
            .ToList();
    }

    public List<Booking> GetAllBookings()
    {
        return bookings;
    }
}
