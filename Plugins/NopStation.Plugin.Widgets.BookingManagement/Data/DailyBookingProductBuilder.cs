using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;
public class DailyBookingProductBuilder : NopEntityBuilder<DailyBookingProduct>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(DailyBookingProduct.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(DailyBookingProduct.ProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(DailyBookingProduct.Date)).AsDateTime().NotNullable();
    }
}
