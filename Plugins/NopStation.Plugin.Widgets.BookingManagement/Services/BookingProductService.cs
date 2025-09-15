using Nop.Data;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public class BookingProductService : IBookingProductService
{
    #region Fields

    private readonly IRepository<BookingProduct> _bookingProductRepository;

    #endregion

    #region Ctor
    public BookingProductService(IRepository<BookingProduct> bookingProductRepository)
    {
        _bookingProductRepository = bookingProductRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteBookingProductAsync(BookingProduct bookingProduct)
    {
        ArgumentNullException.ThrowIfNull(bookingProduct);

        await _bookingProductRepository.DeleteAsync(bookingProduct);
    }

    public async Task<BookingProduct> GetBookingProductByIdAsync(int bookingProductId)
    {
        return await _bookingProductRepository.GetByIdAsync(bookingProductId);
    }

    public async Task InsertBookingProductAsync(BookingProduct bookingProduct)
    {
        ArgumentNullException.ThrowIfNull(bookingProduct);
        await _bookingProductRepository.InsertAsync(bookingProduct);
    }

    public async Task UpdateBookingProductAsync(BookingProduct bookingProduct)
    {
        ArgumentNullException.ThrowIfNull(bookingProduct);
        await _bookingProductRepository.UpdateAsync(bookingProduct);
    }


    #endregion
}
