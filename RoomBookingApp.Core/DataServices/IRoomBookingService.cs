using RoomBookingApp.Domain;

namespace RoomBookingApp.Core.DataServices
{
    public interface IRoomBookingService
    {
        IEnumerable<Room> GetRooms();
        void SaveBooking(RoomBooking roomBooking);
        IEnumerable<Room> GetAvailableRooms(DateTime date);      
    }
}
