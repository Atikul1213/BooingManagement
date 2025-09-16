using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;
public class BookingSlotCapacityBuilder : NopEntityBuilder<BookingSlotCapacity>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(BookingSlotCapacity.Id)).AsInt32().PrimaryKey()
            .WithColumn(nameof(BookingSlotCapacity.ProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(BookingSlotCapacity.SlotBookingProductId)).AsInt32().NotNullable();
    }
}
