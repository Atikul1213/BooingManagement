using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Nop.Services.Catalog;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Models;
using NopStation.Plugin.Widgets.BookingManagement.Domains;
using NopStation.Plugin.Widgets.BookingManagement.Services;

namespace NopStation.Plugin.Widgets.BookingManagement.Infrastructure;

public class EventConsumer : IConsumer<ModelReceivedEvent<BaseNopModel>>
{
    #region Properties

    private readonly IProductService _productService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedEntityService _localizedEntityService;
    private readonly IBookingProductService _bookingProductService;

    #endregion

    #region Ctor

    public EventConsumer(IProductService productService,
        IHttpContextAccessor httpContextAccessor,
        ILanguageService languageService,
        ILocalizedEntityService localizedEntityService,
        IBookingProductService bookingProductService)
    {
        _productService = productService;
        _httpContextAccessor = httpContextAccessor;
        _languageService = languageService;
        _localizedEntityService = localizedEntityService;
        _bookingProductService = bookingProductService;
    }

    #endregion

    #region Utilities


    private bool CheckFormValue(string key, out string returnedValue)
    {
        returnedValue = string.Empty;
        if (_httpContextAccessor.HttpContext.Request.Form.TryGetValue(key, out var value)
            && !StringValues.IsNullOrEmpty(value))
        {
            returnedValue = value.FirstOrDefault();
            return true;
        }
        return false;
    }

    private async Task CreateOrUpdateBookingProductAsync(BookingProduct bookingProduct, bool isNew = false)
    {
        if (CheckFormValue("BookByDaily", out var bookByDaily))
            bookingProduct.BookByDaily = Convert.ToBoolean(bookByDaily);

        if (CheckFormValue("BookByTimeSlot", out var returnedValue))
            bookingProduct.BookByTimeSlot = Convert.ToBoolean(returnedValue);

        if (isNew)
            await _bookingProductService.InsertBookingProductAsync(bookingProduct);
        else
            await _bookingProductService.UpdateBookingProductAsync(bookingProduct);

    }

    #endregion

    #region Methods

    public async Task HandleEventAsync(ModelReceivedEvent<BaseNopModel> eventMessage)
    {
        var entity = eventMessage.Model switch
        {
            ProductModel productModel => await _productService.GetProductByIdAsync(productModel.Id),
            _ => null
        };
        if (entity == null)
            return;

        var isBookingProductExists = await _bookingProductService.GetBookingProductByProductIdAsync(entity.Id);
        if (isBookingProductExists == null)
        {
            var bookingProduct = new BookingProduct { ProductId = entity.Id };
            await CreateOrUpdateBookingProductAsync(bookingProduct, true);
        }
        else
            await CreateOrUpdateBookingProductAsync(isBookingProductExists);


    }

    #endregion
}