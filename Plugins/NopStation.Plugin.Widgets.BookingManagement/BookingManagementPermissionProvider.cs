using Nop.Core.Domain.Customers;
using Nop.Services.Security;

namespace NopStation.Plugin.Widgets.BookingManagement;

public class BookingManagementPermissionProvider
{
    public const string MANAGE_CONFIGURATION = "ManageNopStationBookingManagementConfiguration";
    public const string MANAGE_BOOKING_CAPACITY = "ManageNopStationBookingManagementCapacity";
}

public class BookingManagementPermissionConfigManager : IPermissionConfigManager
{
    public IList<PermissionConfig> AllConfigs => new List<PermissionConfig>
    {
        new("NopStation Booking Product. Manage Configuration", BookingManagementPermissionProvider.MANAGE_CONFIGURATION, "NopStation", NopCustomerDefaults.AdministratorsRoleName),
        new("NopStation Booking Product. Manage booking Capacity", BookingManagementPermissionProvider.MANAGE_BOOKING_CAPACITY, "NopStation", NopCustomerDefaults.AdministratorsRoleName),
    };
}