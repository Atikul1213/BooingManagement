using Nop.Data;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Services;
public class SlotBookingProductService : ISlotBookingProductService
{
    #region Fields

    private readonly IRepository<SlotBookingProduct> _slotBookingProductRepository;

    #endregion

    #region Ctor

    public SlotBookingProductService(IRepository<SlotBookingProduct> slotBookingProductRepository)
    {
        _slotBookingProductRepository = slotBookingProductRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteSlotBookingProductAsync(SlotBookingProduct slotBookingProduct)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(slotBookingProduct));
        await _slotBookingProductRepository.DeleteAsync(slotBookingProduct);
    }

    public async Task<SlotBookingProduct> GetSlotBookingProductByIdAsync(int slotBookingProductId)
    {
        return await _slotBookingProductRepository.Table.FirstAsync(sbp => sbp.Id == slotBookingProductId);
    }

    public async Task<IList<SlotBookingProduct>> GetSlotBookingProductsByProductIdAsync(int productId)
    {
        return await _slotBookingProductRepository.Table.Where(sbp => sbp.ProductId == productId).ToListAsync();
    }

    public async Task InsertSlotBookingProductAsync(SlotBookingProduct slotBookingProduct)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(slotBookingProduct));
        await _slotBookingProductRepository.InsertAsync(slotBookingProduct);
    }

    public async Task<bool> IsExistSlotBookingProductsBySlotTimeAndProductIdAsycn(int productId = 0, DateTime startTime = default, DateTime endTime = default)
    {
        var query = _slotBookingProductRepository.Table
       .Where(sbp => sbp.ProductId == productId
                  && sbp.StartTime == startTime
                  && sbp.EndTime == endTime);

        return await query.AnyAsync();
    }

    public async Task UpdateSlotBookingProductAsync(SlotBookingProduct slotBookingProduct)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(slotBookingProduct));
        await _slotBookingProductRepository.UpdateAsync(slotBookingProduct);
    }

    #endregion
}
