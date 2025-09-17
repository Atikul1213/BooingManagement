using Nop.Data;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public class SlotBookingCapacityService : ISlotBookingCapacityService
{
    #region Fields

    private readonly IRepository<SlotBookingCapacity> _slotBookingCapacityRepository;

    #endregion

    #region Ctor

    public SlotBookingCapacityService(IRepository<SlotBookingCapacity> slotBookingCapacityRepository)
    {
        _slotBookingCapacityRepository = slotBookingCapacityRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteSlotBookingCapacityAsync(SlotBookingCapacity slotBookingCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(slotBookingCapacity));
        await _slotBookingCapacityRepository.DeleteAsync(slotBookingCapacity);
    }

    public async Task<SlotBookingCapacity> GetSlotBookingCapacityByIdAsync(int slotBookingCapacityId)
    {
        return await _slotBookingCapacityRepository.Table.FirstAsync(bsc => bsc.Id == slotBookingCapacityId);
    }

    public async Task<IList<SlotBookingCapacity>> GetSlotBookingCapacitysByProductIdAsync(int productId)
    {
        return await _slotBookingCapacityRepository.Table.Where(bsc => bsc.SlotBookingProductId == productId).ToListAsync();
    }

    public async Task InsertSlotBookingCapacityAsync(SlotBookingCapacity slotBookingCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(slotBookingCapacity));

        await _slotBookingCapacityRepository.InsertAsync(slotBookingCapacity);
    }

    public async Task UpdateSlotBookingCapacityAsync(SlotBookingCapacity slotBookingCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(slotBookingCapacity));
        await _slotBookingCapacityRepository.UpdateAsync(slotBookingCapacity);
    }

    #endregion
}
