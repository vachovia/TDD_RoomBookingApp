using RoomBookingApp.Core.Models;
using System;

namespace RoomBookingApp.Core.Domain
{
    public class RoomBooking : RoomBookingBase
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
    }
}