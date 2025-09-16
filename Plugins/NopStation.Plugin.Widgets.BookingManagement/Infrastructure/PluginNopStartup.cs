using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using NopStation.Plugin.Misc.Core.Infrastructure;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Factories;
using NopStation.Plugin.Widgets.BookingManagement.Services;

namespace NopStation.Plugin.Widgets.BookingManagement.Infrastructure;

public class PluginNopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddNopStationServices("NopStation.Plugin.Widgets.BookingManagement");

        services.AddScoped<IBookingProductService, BookingProductService>();
        services.AddScoped<IBookingProductModelFactory, BookingProductModelFactory>();
        services.AddScoped<IDailyBookingProductService, DailyBookingProductService>();
        services.AddScoped<ISlotBookingProductService, SlotBookingProductService>();
        services.AddScoped<IBookingSlotCapacityService, BookingSlotCapacityService>();

    }

    public void Configure(IApplicationBuilder application)
    {
    }

    public int Order => 11;
}