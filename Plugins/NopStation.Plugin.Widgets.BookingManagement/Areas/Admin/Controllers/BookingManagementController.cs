using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NopStation.Plugin.Misc.Core.Controllers;
using NopStation.Plugin.Misc.Core.Filters;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Controllers;
public class BookingManagementController : NopStationAdminController
{
    #region Fields

    private readonly IStoreContext _storeContext;
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IBookingProductModelFactory _bookingProductModelFactory;

    #endregion

    #region Ctor

    public BookingManagementController(IStoreContext storeContext,
        ILocalizationService localizationService,
        INotificationService notificationService,
        IPermissionService permissionService,
        ISettingService settingService,
        IBookingProductModelFactory bookingProductModelFactory)
    {
        _storeContext = storeContext;
        _localizationService = localizationService;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _settingService = settingService;
        _bookingProductModelFactory = bookingProductModelFactory;
    }

    #endregion

    #region Configuration
    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_CONFIGURATION))
            return AccessDeniedView();

        var model = await _bookingProductModelFactory.PrepareConfigurationModelAsync();

        return View(model);
    }

    [EditAccess, HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_CONFIGURATION))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<BookingManagementSettings>(storeScope);
        settings = model.ToSettings(settings);

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.EnableBookingManagement, model.EnableBookingManagement_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.BookByDailyAttributeId, model.BookByDailyAttributeId_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.BookBySlotAttributeId, model.BookBySlotAttributeId_OverrideForStore, storeScope, false);

        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Configuration.Updated"));

        return RedirectToAction("Configure");
    }

    #endregion
}
