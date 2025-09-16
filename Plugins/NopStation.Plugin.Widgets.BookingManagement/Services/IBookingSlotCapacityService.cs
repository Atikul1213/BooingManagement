using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public interface IBookingSlotCapacityService
{
    Task DeleteBookingSlotCapacityAsync(BookingSlotCapacity bookingSlotCapacity);

    Task InsertBookingSlotCapacityAsync(BookingSlotCapacity bookingSlotCapacity);

    Task UpdateBookingSlotCapacityAsync(BookingSlotCapacity bookingSlotCapacity);

    Task<BookingSlotCapacity> GetBookingSlotCapacityByIdAsync(int bookingSlotCapacityId);
    Task<IList<BookingSlotCapacity>> GetBookingSlotCapacitysByProductIdAsync(int productId);
}
