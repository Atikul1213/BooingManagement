using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
public class BookingProductModelFactory : IBookingProductModelFactory
{
    #region Fields

    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;
    private readonly IProductAttributeService _productAttributeService;

    #endregion

    #region Ctor

    public BookingProductModelFactory(ISettingService settingService,
        IStoreContext storeContext,
        IProductAttributeService productAttributeService)
    {
        _settingService = settingService;
        _storeContext = storeContext;
        _productAttributeService = productAttributeService;
    }

    #endregion

    #region Utilities

    private async Task<IList<SelectListItem>> GetProductAttibuteSelectListAsync()
    {
        var availableAttributes = await _productAttributeService.GetAllProductAttributesAsync();

        var list = new List<SelectListItem>();
        foreach (var attribute in availableAttributes)
        {
            list.Add(new SelectListItem()
            {
                Value = attribute.Id.ToString(),
                Text = attribute.Name
            });
        }
        return list;
    }


    #endregion

    #region Methods

    public async Task<ConfigurationModel> PrepareConfigurationModelAsync()
    {
        var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<BookingManagementSettings>(storeId);

        var model = settings.ToSettingsModel<ConfigurationModel>();

        model.ActiveStoreScopeConfiguration = storeId;
        model.AvailableProductAttributes = await GetProductAttibuteSelectListAsync();

        model.AvailableProductAttributes.Insert(0, new SelectListItem()
        {
            Value = "0",
            Text = "Select product attribute"
        });

        if (storeId <= 0)
            return model;

        model.EnableBookingManagement_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.EnableBookingManagement, storeId);

        return model;
    }

    #endregion

}
