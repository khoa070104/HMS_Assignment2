using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class BookingDetailRepo : IBookingDetailRepo
    {
        private HmsDbContext _context;

        public BookingDetailRepo()
        {
            _context = new HmsDbContext();
        }

        public IEnumerable<BookingDetail> GetBookingDetailsByReservationId(int reservationId)
        {
            return _context.BookingDetails
                .Include(bd => bd.Room)
                .Where(bd => bd.BookingReservationID == reservationId)
                .ToList();
        }

        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            _context.SaveChanges();
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Update(bookingDetail);
            _context.SaveChanges();
        }

        public void DeleteBookingDetail(int bookingDetailId)
        {
            var bookingDetail = _context.BookingDetails.Find(bookingDetailId);
            if (bookingDetail != null)
            {
                _context.BookingDetails.Remove(bookingDetail);
                _context.SaveChanges();
            }
        }
    }
}
