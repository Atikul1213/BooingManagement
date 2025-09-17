using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;

public class BookProductsMappingBuilder : NopEntityBuilder<BookingProduct>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(BookingProduct.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(BookingProduct.ProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(BookingProduct.BookByDaily)).AsBoolean().Nullable()
            .WithColumn(nameof(BookingProduct.BookByTimeSlot)).AsBoolean().Nullable();
    }
}