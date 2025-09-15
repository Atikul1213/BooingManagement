using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.Catalog;
using NopStation.Plugin.Misc.Core.Components;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;
using NopStation.Plugin.Widgets.BookingManagement.Services;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Components;
public class BookingProductsViewComponent : NopStationViewComponent
{
    #region Properties

    private readonly IPermissionService _permissionService;
    private readonly IWidgetPluginManager _widgetPluginManager;
    private readonly IProductService _productService;
    private readonly IBookingProductService _bookingProductService;

    #endregion

    #region Ctor

    public BookingProductsViewComponent(IPermissionService permissionService,
        IWidgetPluginManager widgetPluginManager,
        IProductService productService,
        IBookingProductService bookingProductService)
    {
        _permissionService = permissionService;
        _widgetPluginManager = widgetPluginManager;
        _productService = productService;
        _bookingProductService = bookingProductService;
    }

    #endregion

    #region Methods

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        if (additionalData.GetType() != typeof(ProductModel))
            return Content("");

        var productModel = additionalData as ProductModel;

        var product = await _productService.GetProductByIdAsync(productModel.Id);
        if (product == null || product.Deleted)
            return Content("");

        var isBookingProductExists = await _bookingProductService.GetBookingProductByProductIdAsync(productModel.Id);

        var model = new BookingProductModel
        {
            ProductId = product.Id,
        };

        if (isBookingProductExists != null)
        {
            model.Id = isBookingProductExists.Id;
            model.BookByDaily = isBookingProductExists.BookByDaily;
            model.BookByTimeSlot = isBookingProductExists.BookByTimeSlot;
        }

        return View(model);
    }

    #endregion
}
