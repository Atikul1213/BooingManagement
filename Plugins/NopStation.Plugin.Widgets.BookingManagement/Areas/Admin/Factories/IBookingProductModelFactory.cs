using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
public interface IBookingProductModelFactory
{
    Task<ConfigurationModel> PrepareConfigurationModelAsync();
}
