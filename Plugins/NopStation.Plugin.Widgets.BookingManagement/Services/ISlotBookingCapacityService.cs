using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public interface ISlotBookingCapacityService
{
    Task DeleteSlotBookingCapacityAsync(SlotBookingCapacity slotBookingCapacity);

    Task InsertSlotBookingCapacityAsync(SlotBookingCapacity slotBookingCapacity);

    Task UpdateSlotBookingCapacityAsync(SlotBookingCapacity slotBookingCapacity);

    Task<SlotBookingCapacity> GetSlotBookingCapacityByIdAsync(int slotBookingCapacityId);
    Task<IList<SlotBookingCapacity>> GetSlotBookingCapacitysByProductIdAsync(int productId);
}
