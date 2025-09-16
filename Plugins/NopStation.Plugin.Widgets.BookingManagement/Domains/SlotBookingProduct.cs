using Nop.Core;

namespace NopStation.Plugin.Widgets.BookingManagement.Domains;
public class SlotBookingProduct : BaseEntity
{
    public int ProductId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
