using Nop.Data;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public class DailyBookingCapacityService : IDailyBookingCapacityService
{
    #region Fields

    private readonly IRepository<DailyBookingCapacity> _dailyBookingCapacityRepository;

    #endregion

    #region Ctor

    public DailyBookingCapacityService(IRepository<DailyBookingCapacity> dailyBookingCapacityRepository)
    {
        _dailyBookingCapacityRepository = dailyBookingCapacityRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteDailyBookingCapacityAsync(DailyBookingCapacity dailyBookingCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dailyBookingCapacity));
        await _dailyBookingCapacityRepository.DeleteAsync(dailyBookingCapacity);
    }

    public async Task<DailyBookingCapacity> GetDailyBookingCapacityByIdAsync(int dailyBookingCapacityId)
    {
        return await _dailyBookingCapacityRepository.Table.FirstAsync(bdc => bdc.Id == dailyBookingCapacityId);
    }

    public async Task<DailyBookingCapacity> GetDailyBookingCapacitysByProductIdAsync(int productId)
    {
        return await _dailyBookingCapacityRepository.Table.FirstOrDefaultAsync(bdc => bdc.ProductId == productId);
    }

    public async Task InsertDailyBookingCapacityAsync(DailyBookingCapacity dailyBookingCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dailyBookingCapacity));
        await _dailyBookingCapacityRepository.InsertAsync(dailyBookingCapacity);
    }

    public async Task UpdateDailyBookingCapacityAsync(DailyBookingCapacity dailyBookingCapacity)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dailyBookingCapacity));
        await _dailyBookingCapacityRepository.UpdateAsync(dailyBookingCapacity);
    }

    #endregion
}
