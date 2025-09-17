using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Shipping;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models.DailyBookingCapacity;
using NopStation.Plugin.Widgets.BookingManagement.Services;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
public class DailyCapacityModelFactory : IDailyCapacityModelFactory
{
    #region Fields

    private readonly ILocalizationService _localizationService;
    private readonly IShippingService _shippingService;
    private readonly IWebHelper _webHelper;
    private readonly IStoreContext _storeContext;
    private readonly IBookingProductService _bookingProductService;
    private readonly IProductService _productService;
    private readonly IDailyBookingCapacityService _dailyBookingCapacityService;

    #endregion

    #region Ctor

    public DailyCapacityModelFactory(ILocalizationService localizationService,
        IShippingService shippingService,
        IWebHelper webHelper,
        IStoreContext storeContext,
        IBookingProductService bookingProductService,
        IProductService productService,
        IDailyBookingCapacityService dailyBookingCapacityService)
    {
        _shippingService = shippingService;
        _webHelper = webHelper;
        _storeContext = storeContext;
        _bookingProductService = bookingProductService;
        _productService = productService;
        _dailyBookingCapacityService = dailyBookingCapacityService;
    }

    #endregion

    #region Methods

    public async Task<DailyBookingCapacityModel> PrepareDailyBookingCapacityModelAsync(int productId = 0)
    {
        var model = new DailyBookingCapacityModel();
        model.ProductId = productId;

        var productCapacity = await _dailyBookingCapacityService.GetDailyBookingCapacitysByProductIdAsync(productId);
        if (productCapacity != null)
            model = productCapacity.ToModel<DailyBookingCapacityModel>();

        var dailyBookingProductIds = await _bookingProductService.GetAllDailyBookingProductProductIdsAsync();
        var dailyBookingProducts = await _productService.GetProductsByIdsAsync(dailyBookingProductIds.ToArray());

        model.DailyBookingProducts = dailyBookingProducts.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString(),
            Selected = x?.Id == productId
        }).ToList();

        return model;
    }

    #endregion
}
