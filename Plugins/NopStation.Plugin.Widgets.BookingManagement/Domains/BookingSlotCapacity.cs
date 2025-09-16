using Nop.Core;

namespace NopStation.Plugin.Widgets.BookingManagement.Domains;
public class BookingSlotCapacity : BaseEntity
{
    public int ProductId { get; set; }
    public int SlotBookingProductId { get; set; }

    public int Day1Capacity { get; set; }

    public int Day2Capacity { get; set; }

    public int Day3Capacity { get; set; }

    public int Day4Capacity { get; set; }

    public int Day5Capacity { get; set; }

    public int Day6Capacity { get; set; }

    public int Day7Capacity { get; set; }
    public int Day1BookedSlot { get; set; }

    public int Day2BookedSlot { get; set; }

    public int Day3BookedSlot { get; set; }

    public int Day4BookedSlot { get; set; }

    public int Day5BookedSlot { get; set; }

    public int Day6BookedSlot { get; set; }

    public int Day7BookedSlot { get; set; }
}
