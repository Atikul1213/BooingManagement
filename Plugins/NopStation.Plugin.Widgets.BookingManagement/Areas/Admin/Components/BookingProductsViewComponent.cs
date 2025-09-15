using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.Catalog;
using NopStation.Plugin.Misc.Core.Components;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Components;
public class BookingProductsViewComponent : NopStationViewComponent
{
    #region Properties

    private readonly IPermissionService _permissionService;
    private readonly IWidgetPluginManager _widgetPluginManager;
    private readonly IProductService _productService;

    #endregion

    #region Ctor

    public BookingProductsViewComponent(IPermissionService permissionService,
        IWidgetPluginManager widgetPluginManager,
        IProductService productService)
    {
        _permissionService = permissionService;
        _widgetPluginManager = widgetPluginManager;
        _productService = productService;
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

        var model = new BookingProductModel
        {
            ProductId = product.Id,

        };

        return View(model);
    }

    #endregion
}
