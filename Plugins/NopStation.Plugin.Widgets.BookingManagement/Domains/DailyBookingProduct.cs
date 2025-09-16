using Nop.Core;

namespace NopStation.Plugin.Widgets.BookingManagement.Domains;
public class DailyBookingProduct : BaseEntity
{
    public int ProductId { get; set; }
    public DateTime Date { get; set; }
}
