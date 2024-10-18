using Repositories;

namespace Services
{
    public static class ServiceProvider
    {
        private static IRoomRepo _roomRepo = new RoomRepo();
        private static IRoomTypeRepo _roomTypeRepo = new RoomTypeRepo();
        private static IBookingRepo _bookingRepo = new BookingRepo();
        private static ICustomerRepo _customerRepo = new CustomerRepo();

        public static IRoomService GetRoomService()
        {
            return new RoomService(_roomRepo, _roomTypeRepo, _bookingRepo, _customerRepo);
        }

        public static ICustomerService GetCustomerService()
        {
            return new CustomerService(new CustomerRepo());
        }

        public static IRoomTypeService GetRoomTypeService()
        {
            return new RoomTypeService(_roomTypeRepo);
        }
    }
}
