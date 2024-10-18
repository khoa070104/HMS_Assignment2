public interface IBookingRepo
{
    void AddBooking(Booking booking);
    List<Booking> GetBookingsByCustomer(int customerId);
    List<int> GetBookedRooms(DateTime checkIn, DateTime checkOut);
    List<Booking> GetAllBookings();
}
