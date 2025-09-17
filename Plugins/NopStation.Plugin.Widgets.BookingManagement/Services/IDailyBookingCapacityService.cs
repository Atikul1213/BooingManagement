using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public interface IDailyBookingCapacityService
{
    Task DeleteDailyBookingCapacityAsync(DailyBookingCapacity dailyBookingCapacity);

    Task InsertDailyBookingCapacityAsync(DailyBookingCapacity dailyBookingCapacity);

    Task UpdateDailyBookingCapacityAsync(DailyBookingCapacity dailyBookingCapacity);

    Task<DailyBookingCapacity> GetDailyBookingCapacityByIdAsync(int dailyBookingCapacityId);
    Task<DailyBookingCapacity> GetDailyBookingCapacitysByProductIdAsync(int productId);
}
