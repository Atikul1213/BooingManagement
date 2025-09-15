using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace NopStation.Plugin.Widgets.BookingManagement.Infrastructure;

public class RouteProvider : BaseRouteProvider, IRouteProvider
{
    public int Priority => 1;

    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var pattern = GetLanguageRoutePattern();

        endpointRouteBuilder.MapControllerRoute("DeliverySlots", pattern + "deliveryslots",
            new { controller = "BookingManagement", action = "DeliverySlots" });

        endpointRouteBuilder.MapControllerRoute("SaveDeliverySlot", pattern + "savedeliveryslot",
            new { controller = "BookingManagement", action = "SaveDeliverySlot" });
    }
}
