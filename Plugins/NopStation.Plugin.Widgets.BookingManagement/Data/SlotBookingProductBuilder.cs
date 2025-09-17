using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;
public class SlotBookingProductBuilder : NopEntityBuilder<SlotBookingProduct>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(SlotBookingProduct.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(SlotBookingProduct.ProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(SlotBookingProduct.StartTime)).AsDateTime().NotNullable()
            .WithColumn(nameof(SlotBookingProduct.EndTime)).AsDateTime().NotNullable();
    }
}
