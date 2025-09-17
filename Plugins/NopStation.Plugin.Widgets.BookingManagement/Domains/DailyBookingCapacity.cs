using Nop.Core;

namespace NopStation.Plugin.Widgets.BookingManagement.Domains;
public class DailyBookingCapacity : BaseEntity
{
    public int ProductId { get; set; }

    public int Day1Capacity { get; set; }

    public int Day2Capacity { get; set; }

    public int Day3Capacity { get; set; }

    public int Day4Capacity { get; set; }

    public int Day5Capacity { get; set; }

    public int Day6Capacity { get; set; }

    public int Day7Capacity { get; set; }
}
