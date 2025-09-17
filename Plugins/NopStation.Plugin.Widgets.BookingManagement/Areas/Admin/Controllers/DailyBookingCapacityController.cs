using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NopStation.Plugin.Misc.Core.Controllers;
using NopStation.Plugin.Misc.Core.Filters;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models.DailyBookingCapacity;
using NopStation.Plugin.Widgets.BookingManagement.Domains;
using NopStation.Plugin.Widgets.BookingManagement.Services;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Controllers;
public class DailyBookingCapacityController : NopStationAdminController
{
    #region Fields

    private readonly IPermissionService _permissionService;
    private readonly INotificationService _notificationService;
    private readonly ILocalizationService _localizationService;
    private readonly IShippingService _shippingService;
    private readonly IStoreContext _storeContext;
    private readonly IProductService _productService;
    private readonly IDailyCapacityModelFactory _dailyCapacityModelFactory;
    private readonly IDailyBookingCapacityService _dailyBookingCapacityService;

    #endregion

    #region Ctor

    public DailyBookingCapacityController(IPermissionService permissionService,
        INotificationService notificationService,
        ILocalizationService localizationService,
        IShippingService shippingService,
        IStoreContext storeContext,
        IProductService productService,
        IDailyCapacityModelFactory dailyCapacityModelFactory,
        IDailyBookingCapacityService dailyBookingCapacityService)
    {
        _permissionService = permissionService;
        _notificationService = notificationService;
        _localizationService = localizationService;
        _shippingService = shippingService;
        _storeContext = storeContext;
        _productService = productService;
        _dailyCapacityModelFactory = dailyCapacityModelFactory;
        _dailyBookingCapacityService = dailyBookingCapacityService;
    }

    #endregion

    #region Methods

    public async Task<IActionResult> Configure(int productId = 0)
    {
        if (!await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_BOOKING_CAPACITY))
            return AccessDeniedView();

        var product = await _productService.GetProductByIdAsync(productId);


        var model = await _dailyCapacityModelFactory.PrepareDailyBookingCapacityModelAsync(productId);
        return View(model);
    }

    [EditAccess, HttpPost]
    public async Task<IActionResult> Configure(DailyBookingCapacityModel model)
    {
        if (!await _permissionService.AuthorizeAsync(BookingManagementPermissionProvider.MANAGE_BOOKING_CAPACITY))
            return AccessDeniedView();

        var product = await _productService.GetProductByIdAsync(model.ProductId);
        if (product == null)
        {
            _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("NopStation.Plugin.Widgets.BookingManagement.DailyBookingCapacity.Product.NotFound"));
            return RedirectToAction("Configure");
        }

        var prevDailyBookingCapacity = await _dailyBookingCapacityService.GetDailyBookingCapacitysByProductIdAsync(model.ProductId);

        if (prevDailyBookingCapacity == null)
        {
            var dailyBookingCapacity = model.ToEntity<DailyBookingCapacity>();
            await _dailyBookingCapacityService.InsertDailyBookingCapacityAsync(dailyBookingCapacity);
        }
        else
        {
            prevDailyBookingCapacity.Day1Capacity = model.Day1Capacity;
            prevDailyBookingCapacity.Day2Capacity = model.Day2Capacity;
            prevDailyBookingCapacity.Day3Capacity = model.Day3Capacity;
            prevDailyBookingCapacity.Day4Capacity = model.Day4Capacity;
            prevDailyBookingCapacity.Day5Capacity = model.Day5Capacity;
            prevDailyBookingCapacity.Day6Capacity = model.Day6Capacity;
            prevDailyBookingCapacity.Day7Capacity = model.Day7Capacity;

            await _dailyBookingCapacityService.UpdateDailyBookingCapacityAsync(prevDailyBookingCapacity);
        }

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("NopStation.Plugin.Widgets.BookingManagement.DailyBookingCapacity.Product.Updated"));
        return RedirectToAction("Configure", new { productId = product.Id });
    }

    #endregion
}
