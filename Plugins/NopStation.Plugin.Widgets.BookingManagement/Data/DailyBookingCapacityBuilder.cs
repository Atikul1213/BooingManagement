using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;
public class DailyBookingCapacityBuilder : NopEntityBuilder<DailyBookingCapacity>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(DailyBookingCapacity.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(DailyBookingCapacity.ProductId)).AsInt32().NotNullable();
    }
}
