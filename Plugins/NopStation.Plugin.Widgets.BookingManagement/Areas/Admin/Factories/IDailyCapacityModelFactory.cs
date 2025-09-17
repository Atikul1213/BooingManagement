using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models.DailyBookingCapacity;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
public interface IDailyCapacityModelFactory
{
    Task<DailyBookingCapacityModel> PrepareDailyBookingCapacityModelAsync(int productId);
}
