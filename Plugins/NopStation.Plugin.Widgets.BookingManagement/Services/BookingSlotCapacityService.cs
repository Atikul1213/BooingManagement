using Nop.Data;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public class BookingSlotCapacityService : IBookingSlotCapacityService
{
    #region Fields

    private readonly IRepository<BookingSlotCapacity> _bookingSlotCapacityRepository;

    #endregion

    #region Ctor

    public BookingSlotCapacityService(IRepository<BookingSlotCapacity> bookingSlotCapacityRepository)
    {
        _bookingSlotCapacityRepository = bookingSlotCapacityRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteBookingSlotCapacityAsync(BookingSlotCapacity bookingSlotCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(bookingSlotCapacity));
        await _bookingSlotCapacityRepository.DeleteAsync(bookingSlotCapacity);
    }

    public async Task<BookingSlotCapacity> GetBookingSlotCapacityByIdAsync(int bookingSlotCapacityId)
    {
        return await _bookingSlotCapacityRepository.Table.FirstAsync(bsc => bsc.Id == bookingSlotCapacityId);
    }

    public async Task<IList<BookingSlotCapacity>> GetBookingSlotCapacitysByProductIdAsync(int productId)
    {
        return await _bookingSlotCapacityRepository.Table.Where(bsc => bsc.SlotBookingProductId == productId).ToListAsync();
    }

    public async Task InsertBookingSlotCapacityAsync(BookingSlotCapacity bookingSlotCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(bookingSlotCapacity));

        await _bookingSlotCapacityRepository.InsertAsync(bookingSlotCapacity);
    }

    public async Task UpdateBookingSlotCapacityAsync(BookingSlotCapacity bookingSlotCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(bookingSlotCapacity));
        await _bookingSlotCapacityRepository.UpdateAsync(bookingSlotCapacity);
    }

    #endregion
}
