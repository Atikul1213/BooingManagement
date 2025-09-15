using Nop.Core;

namespace NopStation.Plugin.Widgets.BookingManagement.Domains;
public class BookingProduct : BaseEntity
{
    public int ProductId { get; set; }
    public bool BookByDaily { get; set; }
    public bool BookByTimeSlot { get; set; }
}
