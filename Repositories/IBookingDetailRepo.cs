using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookingDetailRepo
    {
        IEnumerable<BookingDetail> GetBookingDetailsByReservationId(int reservationId);
        void AddBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(int bookingDetailId);
    }
}
