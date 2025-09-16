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
        if (widgetZone == AdminWidgetZones.ProductDetailsBlock)
            return typeof(BookingProductsViewComponent);

        return typeof(Nullable);
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>
        {
            AdminWidgetZones.ProductDetailsBlock,
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
            ["Admin.NopStation.BookingManagement.Configuration.Fields.EnableBookingManagement"] = "Enable booking management",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.EnableBookingManagement.Hint"] = "Enable booking management",
            ["Admin.NopStation.BookingManagement.BookingProductModel.Fields.BookByDaily"] = "Book by daily",
            ["Admin.NopStation.BookingManagement.BookingProductModel.Fields.BookByDaily.Hint"] = "Check for enable book by daily",
            ["Admin.NopStation.BookingManagement.BookingProductModel.Fields.BookByTimeSlot"] = "Book by time slot",
            ["Admin.NopStation.BookingManagement.BookingProductModel.Fields.BookByTimeSlot.Hint"] = "Check for enable book by time slot",
            ["Admin.NopStation.BookingManagement.Configuration.Settings"] = "Settings",
            ["Admin.NopStation.BookingManagement.Configuration"] = "Booking management settings",
            ["Admin.NopStation.BookingManagement.Tab.Enable"] = "Enable product booking",
            ["Admin.NopStation.BookingManagement.Menu.BookingManagement"] = "Booking management",
            ["Admin.NopStation.BookingManagement.Menu.Configuration"] = "Configuration",
            ["Admin.NopStation.BookingManagement.Menu.BookingCapacities"] = "Booking capacities",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.BookByDailyAttributeId"] = "Book by daily attribute id",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.BookByDailyAttributeId.Hint"] = "Select book by daily attribute id",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.BookBySlotAttributeId"] = "Book by slot attribute id",
            ["Admin.NopStation.BookingManagement.Configuration.Fields.BookBySlotAttributeId.Hint"] = "Select book by slot attribute id",


        };

        return list;
    }

    #endregion
}
