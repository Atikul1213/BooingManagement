using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;
public record BookingProductModel : BaseNopEntityModel
{
    public int ProductId { get; set; }
    [NopResourceDisplayName("Admin.NopStation.BookingManagement.BookingProductModel.Fields.BookByDaily")]
    public bool BookByDaily { get; set; }
    [NopResourceDisplayName("Admin.NopStation.BookingManagement.BookingProductModel.Fields.BookByTimeSlot")]
    public bool BookByTimeSlot { get; set; }
}
