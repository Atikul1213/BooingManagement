using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public interface IDailyBookingProductService
{
    Task DeleteDailyBookingProductAsync(DailyBookingProduct dailyBookingProduct);

    Task InsertDailyBookingProductAsync(DailyBookingProduct dailyBookingProduct);

    Task UpdateDailyBookingProductAsync(DailyBookingProduct dailyBookingProduct);

    Task<DailyBookingProduct> GetDailyBookingProductByIdAsync(int dailyBookingProductId);
    Task<IList<DailyBookingProduct>> GetDailyBookingProductsByProductIdAsync(int productId);
}
