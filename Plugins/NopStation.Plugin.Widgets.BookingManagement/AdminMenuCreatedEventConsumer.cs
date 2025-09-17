using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Menu;
using NopStation.Plugin.Misc.Core;
using NopStation.Plugin.Misc.Core.Infrastructure;

namespace NopStation.Plugin.Widgets.BookingManagement;
public class AdminMenuCreatedEventConsumer : IConsumer<AdminMenuEvent>
{
    #region Fields

    private readonly ILocalizationService _localizationService;
    private readonly IPermissionService _permissionService;

    #endregion

    #region Ctor

    public AdminMenuCreatedEventConsumer(ILocalizationService localizationService, IPermissionService permissionService)
    {
        _localizationService = localizationService;
        _permissionService = permissionService;
    }

    #endregion

    #region Methods

    public async Task HandleEventAsync(AdminMenuEvent createdEvent)
    {
        var menuItem = new NopStationAdminMenuItem()
        {
            Title = await _localizationService.GetResourceAsync("Admin.NopStation.BookingManagement.Menu.BookingManagement"),
            Visible = true,
            IconClass = "far fa-dot-circle",
        };
        if (await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_CONFIGURATION))
        {
            var configItem = new AdminMenuItem()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.BookingManagement.Menu.Configuration"),
                Url = "~/Admin/BookingManagement/Configure",
                Visible = true,
                IconClass = "far fa-circle",
                SystemName = "BookingManagement.Configuration"
            };
            menuItem.ChildNodes.Add(configItem);
        }


        if (await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_BOOKING_CAPACITY))
        {
            var manageDeliveryDailyCapacities = new AdminMenuItem()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.BookingManagement.Menu.DailyBookingCapacities"),
                Url = "~/Admin/DailyBookingCapacity/Configure",
                Visible = true,
                IconClass = "far fa-circle",
                SystemName = "BookingManagement.DailyBookingCapacities"
            };
            menuItem.ChildNodes.Add(manageDeliveryDailyCapacities);
        }

        if (await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_BOOKING_CAPACITY))
        {
            var manageDeliveryDateSlot = new AdminMenuItem()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.BookingManagement.Menu.SlotBookingCapacities"),
                Url = "~/Admin/SlotBookingCapacity/Configure",
                Visible = true,
                IconClass = "far fa-circle",
                SystemName = "BookingManagement.SlotBookingCapacities"
            };
            menuItem.ChildNodes.Add(manageDeliveryDateSlot);
        }


        if (menuItem.ChildNodes.Any())
        {
            if (await _permissionService.AuthorizeAsync(CorePermissionProvider.SHOW_DOCUMENTATIONS))
            {
                var documentation = new AdminMenuItem()
                {
                    Title = await _localizationService.GetResourceAsync("Admin.NopStation.Common.Menu.Documentation"),
                    Url = "https://www.nop-station.com/delivery-scheduler-documentation?utm_source=admin-panel&utm_medium=products&utm_campaign=delivery-scheduler",
                    Visible = true,
                    IconClass = "far fa-circle",
                    OpenUrlInNewTab = true
                };
                menuItem.ChildNodes.Add(documentation);
            }

            createdEvent.PluginChildNodes.Add(menuItem);
        }
    }

    #endregion
}