using Nop.Core;
using Nop.Services.Configuration;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
public class BookingProductModelFactory : IBookingProductModelFactory
{
    #region Fields

    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    #endregion

    #region Ctor

    public BookingProductModelFactory(
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    #endregion

    #region Methods

    public async Task<ConfigurationModel> PrepareConfigurationModelAsync()
    {
        var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<BookingManagementSettings>(storeId);

        var model = settings.ToSettingsModel<ConfigurationModel>();

        model.ActiveStoreScopeConfiguration = storeId;

        if (storeId <= 0)
            return model;

        model.EnableBookingManagement_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.EnableBookingManagement, storeId);

        return model;
    }

    #endregion

}
