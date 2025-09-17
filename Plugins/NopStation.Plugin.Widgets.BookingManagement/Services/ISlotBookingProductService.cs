using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public interface ISlotBookingProductService
{
    Task DeleteSlotBookingProductAsync(SlotBookingProduct slotBookingProduct);

    Task InsertSlotBookingProductAsync(SlotBookingProduct slotBookingProduct);

    Task UpdateSlotBookingProductAsync(SlotBookingProduct slotBookingProduct);

    Task<SlotBookingProduct> GetSlotBookingProductByIdAsync(int slotBookingProductId);
    Task<IList<SlotBookingProduct>> GetSlotBookingProductsByProductIdAsync(int productId);
    Task<bool> IsExistSlotBookingProductsBySlotTimeAndProductIdAsycn(int productId = 0, DateTime startTime = default, DateTime endTime = default);
}
