using System.Globalization;
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
    private readonly IProductAttributeService _productAttributeService;
    private readonly BookingManagementSettings _bookingManagementSettings;
    private readonly IProductAttributeParser _productAttributeParser;
    private readonly ISlotBookingProductService _slotBookingProductService;
    private readonly IDailyBookingProductService _dailyBookingProductService;

    #endregion

    #region Ctor

    public EventConsumer(IProductService productService,
        IHttpContextAccessor httpContextAccessor,
        ILanguageService languageService,
        ILocalizedEntityService localizedEntityService,
        IBookingProductService bookingProductService,
        IProductAttributeService productAttributeService,
        BookingManagementSettings bookingManagementSettings,
        IProductAttributeParser productAttributeParser,
        ISlotBookingProductService slotBookingProductService,
        IDailyBookingProductService dailyBookingProductService)
    {
        _productService = productService;
        _httpContextAccessor = httpContextAccessor;
        _languageService = languageService;
        _localizedEntityService = localizedEntityService;
        _bookingProductService = bookingProductService;
        _productAttributeService = productAttributeService;
        _bookingManagementSettings = bookingManagementSettings;
        _productAttributeParser = productAttributeParser;
        _slotBookingProductService = slotBookingProductService;
        _dailyBookingProductService = dailyBookingProductService;
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


    private async Task InsertProductSlotIntoSlotBookingProductAsync(int productId)
    {
        if (productId > 0)
        {
            var attributes = await _productAttributeService.GetProductAttributeMappingsByProductIdAsync(productId).Result.Where(pam => pam.ProductAttributeId == _bookingManagementSettings.BookBySlotAttributeId).ToListAsync();

            if (attributes.Any())
            {
                var productAttributeValue = await _productAttributeService.GetProductAttributeValuesAsync(attributes[0].Id);

                if (productAttributeValue.Count() > 0)
                {
                    foreach (var pav in productAttributeValue)
                    {
                        string[] parts = pav.Name.Split('-');
                        string startStr = parts[0].Trim();
                        string endStr = parts[1].Trim();

                        TimeSpan startTime = TimeSpan.ParseExact(startStr, "hh\\:mm", CultureInfo.InvariantCulture);
                        TimeSpan endTime = TimeSpan.ParseExact(endStr, "hh\\:mm", CultureInfo.InvariantCulture);

                        DateTime today = DateTime.Today;

                        DateTime startDateTime = today.Add(startTime);
                        DateTime endDateTime = today.Add(endTime);

                        bool isExist = await _slotBookingProductService.IsExistSlotBookingProductsBySlotTimeAndProductIdAsycn(productId, startDateTime, endDateTime);
                        if (!isExist)
                        {
                            var slotBookingProduct = new SlotBookingProduct
                            {
                                ProductId = productId,
                                StartTime = startDateTime,
                                EndTime = endDateTime
                            };
                            await _slotBookingProductService.InsertSlotBookingProductAsync(slotBookingProduct);
                        }
                    }
                }

            }
        }
    }

    private async Task CreateOrUpdateBookingProductAsync(BookingProduct bookingProduct, bool isNew = false, int productId = 0)
    {
        if (CheckFormValue("BookByDaily", out var bookByDaily))
            bookingProduct.BookByDaily = Convert.ToBoolean(bookByDaily);

        if (CheckFormValue("BookByTimeSlot", out var bookByTimeSlot))
            bookingProduct.BookByTimeSlot = Convert.ToBoolean(bookByTimeSlot);

        if (isNew == true && bookingProduct.BookByDaily == false && bookingProduct.BookByTimeSlot == false)
            return;

        if (bookingProduct.BookByTimeSlot)
        {
            var existDailyBookingProduct = await _dailyBookingProductService.GetDailyBookingProductsByProductIdAsync(productId);
            if (existDailyBookingProduct.Any())
            {
                foreach (var dbp in existDailyBookingProduct)
                    await _dailyBookingProductService.DeleteDailyBookingProductAsync(dbp);
            }
        }
        else
        {
            var existSlotBookingProduct = await _slotBookingProductService.GetSlotBookingProductsByProductIdAsync(productId);
            if (existSlotBookingProduct.Any())
            {
                foreach (var sbp in existSlotBookingProduct)
                    await _slotBookingProductService.DeleteSlotBookingProductAsync(sbp);
            }
        }


        if (isNew)
        {
            await _bookingProductService.InsertBookingProductAsync(bookingProduct);
        }
        else
        {
            await _bookingProductService.UpdateBookingProductAsync(bookingProduct);
        }

        if (bookingProduct.BookByTimeSlot)
        {
            await InsertProductSlotIntoSlotBookingProductAsync(productId);
        }
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
            await CreateOrUpdateBookingProductAsync(bookingProduct, true, entity.Id);
        }
        else
            await CreateOrUpdateBookingProductAsync(isBookingProductExists, false, entity.Id);


    }

    #endregion
}