using Nop.Data;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public class DailyBookingProductService : IDailyBookingProductService
{
    #region Fields

    private readonly IRepository<DailyBookingProduct> _dailyBookingProductRepository;

    #endregion

    #region Ctor

    public DailyBookingProductService(IRepository<DailyBookingProduct> dailyBookingProductRepository)
    {
        _dailyBookingProductRepository = dailyBookingProductRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteDailyBookingProductAsync(DailyBookingProduct dailyBookingProduct)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dailyBookingProduct));

        await _dailyBookingProductRepository.DeleteAsync(dailyBookingProduct);
    }

    public async Task<DailyBookingProduct> GetDailyBookingProductByIdAsync(int dailyBookingProductId)
    {
        return await _dailyBookingProductRepository.Table.FirstOrDefaultAsync(dbp => dbp.Id == dailyBookingProductId);
    }

    public async Task<List<DailyBookingProduct>> GetDailyBookingProductsByProductIdAsync(int productId)
    {
        var query = _dailyBookingProductRepository.Table.Where(dbp => dbp.ProductId == productId);
        if (!query.Any())
            return new List<DailyBookingProduct>();

        return await query.ToListAsync();
    }

    public async Task InsertDailyBookingProductAsync(DailyBookingProduct dailyBookingProduct)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dailyBookingProduct));
        await _dailyBookingProductRepository.InsertAsync(dailyBookingProduct);
    }

    public async Task UpdateDailyBookingProductAsync(DailyBookingProduct dailyBookingProduct)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dailyBookingProduct));
        await _dailyBookingProductRepository.UpdateAsync(dailyBookingProduct);
    }

    #endregion
}
