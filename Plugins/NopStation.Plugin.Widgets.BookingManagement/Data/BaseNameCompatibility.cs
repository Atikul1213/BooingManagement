using Nop.Data.Mapping;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;

public class BaseNameCompatibility : INameCompatibility
{
    public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
    {
        { typeof(BookingProduct), "NS_BookingProduct" },
        { typeof(DailyBookingProduct), "NS_DailyBookingProduct" },
        { typeof(SlotBookingProduct), "NS_SlotBookingProduct" },
        { typeof(SlotBookingCapacity), "NS_SlotBookingCapacity" },
        { typeof(DailyBookingCapacity), "NS_DailyBookingCapacity" },

    };

    public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
    {
    };
}
