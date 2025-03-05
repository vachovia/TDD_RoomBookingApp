using System;
using System.Linq;
using RoomBookingApp.Domain;
using System.Collections.Generic;
using RoomBookingApp.Core.DataServices;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;

        public RoomBookingService(RoomBookingAppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            return _context.Rooms.Where(q => !q.RoomBookings.Any(x => x.Date == date)).ToList();
        }

        public IEnumerable<Room> GetRooms()
        {
            throw new NotImplementedException();
        }

        public void SaveBooking(RoomBooking roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
