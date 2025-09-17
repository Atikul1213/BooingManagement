using FluentMigrator;
using Nop.Core;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Migrations;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data.Migrations;
[NopSchemaMigration("2025/09/16 10:47:52:1687541", "Booking Slot Capacity create table schema migration", MigrationProcessType.Update)]
public class SlotBookingCapacityMigration : ForwardOnlyMigration
{
    public static string TableName<T>() where T : BaseEntity
    {
        return NameCompatibilityManager.GetTableName(typeof(T));
    }
    public override void Up()
    {
        if (!Schema.Table(TableName<SlotBookingCapacity>()).Exists())
            Create.TableFor<SlotBookingCapacity>();
    }
}
