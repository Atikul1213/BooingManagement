using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Infrastructure;
using NopStation.Plugin.Misc.Core.Services;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Components;

namespace NopStation.Plugin.Widgets.BookingManagement;

public class BookingManagementPlugin : BasePlugin, IWidgetPlugin, INopStationPlugin
{
    #region Fields

    public bool HideInWidgetList => false;

    private readonly IWebHelper _webHelper;
    private readonly ILocalizationService _localizationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;

    #endregion

    #region Ctor

    public BookingManagementPlugin(IWebHelper webHelper,
        ILocalizationService localizationService,
        IPermissionService permissionService,
        ISettingService settingService)
    {
        _webHelper = webHelper;
        _localizationService = localizationService;
        _permissionService = permissionService;
        _settingService = settingService;
    }

    #endregion

    #region Methods

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/BookingManagement/Configure";
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        if (widgetZone == PublicWidgetZones.OrderDetailsPageOverview)
            return typeof(BookingProductsViewComponent);

        return typeof(Nullable);
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>
        {
            PublicWidgetZones.CheckoutShippingMethodBottom,
            PublicWidgetZones.OpCheckoutShippingMethodBottom,
        });
    }

    public override async Task InstallAsync()
    {
        var setting = new BookingManagementSettings()
        {
            EnableBookingManagement = true,
        };
        await _settingService.SaveSettingAsync(setting);

        await this.InstallPluginAsync();
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await this.UninstallPluginAsync(new BookingManagementPermissionConfigManager());
        await base.UninstallAsync();
    }

    public override async Task UpdateAsync(string currentVersion, string targetVersion)
    {
        if (targetVersion != currentVersion)
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(GetPluginResources());
        }

        await base.UpdateAsync(currentVersion, targetVersion);
    }

    public IDictionary<string, string> GetPluginResources()
    {
        var list = new Dictionary<string, string>
        {
            // menu
            ["Admin.NopStation.BookingManagement.Menu.BookingManagement"] = "Delivery scheduler",
            ["Admin.NopStation.BookingManagement.Menu.Configuration"] = "Configuration",
            ["Admin.NopStation.BookingManagement.Menu.DeliverySlots"] = "Delivery slots",
            ["Admin.NopStation.BookingManagement.Menu.DeliveryCapacities"] = "Delivery capacities",
            ["Admin.NopStation.BookingManagement.Menu.SpecialDeliveryOffsets"] = "Special delivery offsets",
            ["Admin.nopstation.BookingManagement.Menu.OrderList"] = "Orders",
            ["Admin.NopStation.BookingManagement.Menu.SpecialDeliveryCapacities"] = "Special delivery capacities",

            ["Admin.NopStation.BookingManagement.DeliveryCapacities.NoShippingMethod"] = "No shipping method found",

            ["Admin.NopStation.BookingManagement.DeliverySlots.Created"] = "Delivery slot has been created successfully.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Deleted"] = "Delivery slot has been deleted successfully.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Updated"] = "Delivery slot has been updated successfully.",

            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.ShippingMethod"] = "Shipping method",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.ShippingMethod.Hint"] = "The shipping method.",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.DeliverySlot"] = "Delivery slot",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day1Capacity"] = "Sunday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day2Capacity"] = "Monday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day3Capacity"] = "Tuesday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day4Capacity"] = "Wednesday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day5Capacity"] = "Thursday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day6Capacity"] = "Friday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Fields.Day7Capacity"] = "Saturday",
            ["Admin.NopStation.BookingManagement.DeliveryCapacities.DaysOfWeek"] = "Days of week",

            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Created"] = "Special delivery capacities has been created successfully.",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Deleted"] = "Special delivery capacities has been deleted successfully.",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Updated"] = "Special delivery capacities has been updated successfully.",

            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.List"] = "Special capacities",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.AddNew"] = "Add new special capacities",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.BackToList"] = "back to special capacity list",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.EditDetails"] = "Edit special capacity details",

            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.SpecialDate"] = "Special date",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.DeliverySlot"] = "Delivery slot",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.Capacity"] = "Capacity",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.MappedStoreNames"] = "Mapped Store Names",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.Note"] = "Note",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.LimitedToStores"] = "Limited To Stores",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.SpecialDate.Hint"] = "Select special date.",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.ShippingMethod"] = "Shipping method",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.ShippingMethod.Hint"] = "Select shipping method.",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.DeliverySlot.Hint"] = "The delivery slot.",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.Capacity.Hint"] = "Capacity for the special date",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.Note.Hint"] = "Note for special capacity.",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.Fields.LimitedToStores.Hint"] = "Select the stores you want to limit it to",

            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.LimitedToStores"] = "Limited to stores",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.LimitedToStores.Hint"] = "Option to limit this flipbook to a certain store. If you have multiple stores, choose one or several from the list. If you don't use this option just leave this field empty.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.TimeSlot"] = "Time slot",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.DisplayOrder"] = "Display order",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.Active"] = "Active",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.CreatedOn"] = "Created on",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.LimitedToShippingMethods"] = "Limited to shipping methods",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.TimeSlot.Hint"] = "The time slot.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.DisplayOrder.Hint"] = "The display order for this delivery slot. 1 represents the top of the list.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.Active.Hint"] = "Defines whether slot is active or not.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.CreatedOn.Hint"] = "The create date.",
            ["Admin.NopStation.BookingManagement.DeliverySlots.Fields.LimitedToShippingMethods.Hint"] = "Option to limit this delivery slot to a certain shipping method. If you have multiple shipping methods, choose one or several from the list. If you don't use this option just leave this field empty.",

            ["Admin.NopStation.BookingManagement.SpecialDeliveryOffsets.Fields.DaysOffset"] = "Days offset",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryOffsets.Fields.CategoryName"] = "Category",

            ["Admin.NopStation.BookingManagement.DeliveryCapacities"] = "Delivery capacity",
            ["Admin.NopStation.BookingManagement.DeliverySlots.AddNew"] = "Add new delivery slot",
            ["Admin.NopStation.BookingManagement.DeliverySlots.EditDetails"] = "Edit delivery slot details",
            ["Admin.NopStation.BookingManagement.DeliverySlots.BackToList"] = "back to delivery slot list",
            ["Admin.NopStation.BookingManagement.DeliverySlots.List"] = "Delivery slots",

            ["Admin.NopStation.BookingManagement.DeliveryCapacities.Updated"] = "Delivery capacity updated successfully.",

            ["Admin.NopStation.BookingManagement.Configuration"] = "Delivery scheduler settings",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.NumberOfDaysToDisplay"] = "Number of days to display",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.DisplayDayOffset"] = "Display day offset",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.EnableScheduling"] = "Enable scheduling",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.DateFormat"] = "Date format",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.ShowRemainingCapacity"] = "Show remaining capacity",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.NumberOfDaysToDisplay.Hint"] = "Number of days to display",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.DisplayDayOffset.Hint"] = "Display day offset",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.EnableScheduling.Hint"] = "Enable scheduling",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.DateFormat.Hint"] = "The date format to display in checkout page.",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.ShowRemainingCapacity.Hint"] = "Show remaining capacity in checkout page.",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.DateFormat.Sample"] = "(i.e. {0})",

            ["Admin.NopStation.BookingManagement.Configuration.OffsetOverride"] = "Custom offset days",
            ["Admin.NopStation.BookingManagement.Configuration.Settings"] = "Settings",
            ["Admin.NopStation.BookingManagement.Configuration.Button.Reset"] = "Reset",
            ["Admin.NopStation.BookingManagement.SpecialDeliveryCapacities.AlreadyOverridden"] = "Capacity has been already overridden for this date and slot.",

            ["NopStation.BookingManagement.Slots.SlotsAvailable"] = "{0} slots available",
            ["NopStation.BookingManagement.Slots.SlotAvailable"] = "{0} slot available",
            ["NopStation.BookingManagement.Slots.Booked"] = "All Booked",
            ["NopStation.BookingManagement.Slots.NotSelected"] = "Please select a delivery slot",

            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Fields.DeliverySlot"] = "Delivery slot",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Fields.DeliveryDate"] = "Delivery date",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Fields.ShippingMethod"] = "Shipping method",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Fields.DeliverySlot.Hint"] = "The order delivery slot.",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Fields.DeliveryDate.Hint"] = "The order delivery date.",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Fields.ShippingMethod.Hint"] = "The order shipping method.",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.DeliverySlot.All"] = "All",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.ShippingMethod.All"] = "All",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Updated"] = "Order delivery slot updated successfully.",

            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.Info"] = "Delivery info",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List"] = "Orders",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchShippingMethod"] = "Shipping method",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchShippingMethod.Hint"] = "Search by shipping method.",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchDeliverySlot"] = "Delivery slot",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchDeliverySlot.Hint"] = "Search by delivery slot.",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchStartDate"] = "From date",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchStartDate.Hint"] = "Select from date.",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchEndTime"] = "To date",
            ["Admin.NopStation.BookingManagement.OrderDeliverySlots.List.SearchEndTime.Hint"] = "Select to date.",

            ["NopStation.BookingManagement.OrderDeliverySlot.Fields.TimeSlot"] = "Delivery Slot",
            ["NopStation.BookingManagement.OrderDeliverySlot.Fields.ShippingDate"] = "Shipping Date",

        };

        return list;
    }

    #endregion
}
