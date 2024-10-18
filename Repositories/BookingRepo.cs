using System.Collections.Generic;
using System.Linq;

public class BookingRepo : IBookingRepo
{
    private readonly BookingDAO _bookingDao = new BookingDAO();

    public void AddBooking(Booking booking)
    {
        _bookingDao.AddBooking(booking);
    }

    public List<Booking> GetBookingsByCustomer(int customerId)
    {
        return _bookingDao.GetBookingsByCustomer(customerId);
    }

    public List<int> GetBookedRooms(DateTime checkIn, DateTime checkOut)
    {
        return _bookingDao.GetBookedRooms(checkIn, checkOut);
    }

    public List<Booking> GetAllBookings()
    {
        return _bookingDao.GetAllBookings();
    }
}
