using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Data;

[NopMigration("2025/09/15 02:30:55:1687001", "NopStation.BookingManagement base schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<BookingProduct>();
        Create.TableFor<DailyBookingProduct>();
        Create.TableFor<SlotBookingProduct>();
        Create.TableFor<SlotBookingCapacity>();
    }
}