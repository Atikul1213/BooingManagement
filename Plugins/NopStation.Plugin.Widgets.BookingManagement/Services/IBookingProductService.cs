using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public interface IBookingProductService
{
    Task DeleteBookingProductAsync(BookingProduct bookingProduct);

    Task InsertBookingProductAsync(BookingProduct bookingProduct);

    Task UpdateBookingProductAsync(BookingProduct bookingProduct);

    Task<BookingProduct> GetBookingProductByIdAsync(int bookingProductId);
}
