using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly HmsDbContext _context;

        public BookingRepo()
        {
            _context = new HmsDbContext();
        }

        public BookingRepo(HmsDbContext context)
        {
            _context = context;
        }

        public void AddBooking(BookingReservation booking)
        {
            _context.BookingReservations.Add(booking);
            _context.SaveChanges();
        }

        public IEnumerable<BookingReservation> GetBookingsByCustomer(int customerId)
        {
            return _context.BookingReservations
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Room)
                .Where(b => b.CustomerID == customerId)
                .ToList();
        }

        public IEnumerable<int> GetBookedRooms(DateTime checkIn, DateTime checkOut)
        {
            return _context.BookingDetails
                .Where(bd => 
                    (bd.StartDate <= checkIn && checkIn < bd.EndDate) ||
                    (bd.StartDate < checkOut && checkOut <= bd.EndDate) ||
                    (checkIn <= bd.StartDate && bd.EndDate <= checkOut))
                .Select(bd => bd.RoomID)
                .Distinct()
                .ToList();
        }

        public IEnumerable<BookingReservation> GetAllBookings()
        {
            return _context.BookingReservations
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Room)
                .ToList();
        }
    }
}
