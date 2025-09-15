using Nop.Core.Configuration;

namespace NopStation.Plugin.Widgets.BookingManagement;

public class BookingManagementSettings : ISettings
{
    public bool EnableBookingManagement { get; set; }
}