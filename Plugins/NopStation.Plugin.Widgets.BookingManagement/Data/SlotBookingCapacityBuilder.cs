using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;
public class SlotBookingCapacityBuilder : NopEntityBuilder<SlotBookingCapacity>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(SlotBookingCapacity.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(SlotBookingCapacity.ProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(SlotBookingCapacity.SlotBookingProductId)).AsInt32().NotNullable();
    }
}
