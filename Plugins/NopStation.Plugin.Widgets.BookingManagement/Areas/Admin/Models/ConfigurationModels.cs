using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

public partial record ConfigurationModel : BaseNopModel, ISettingsModel
{

    public ConfigurationModel()
    {
        AvailableProductAttributes = new List<SelectListItem>();
    }
    [NopResourceDisplayName("Admin.NopStation.BookingManagement.Configuration.Fields.EnableBookingManagement")]
    public bool EnableBookingManagement { get; set; }
    public bool EnableBookingManagement_OverrideForStore { get; set; }
    [NopResourceDisplayName("Admin.NopStation.BookingManagement.Configuration.Fields.BookByDailyAttributeId")]
    public int BookByDailyAttributeId { get; set; }
    public bool BookByDailyAttributeId_OverrideForStore { get; set; }
    [NopResourceDisplayName("Admin.NopStation.BookingManagement.Configuration.Fields.BookBySlotAttributeId")]
    public int BookBySlotAttributeId { get; set; }
    public bool BookBySlotAttributeId_OverrideForStore { get; set; }
    public int ActiveStoreScopeConfiguration { get; set; }
    public IList<SelectListItem> AvailableProductAttributes { get; set; }
}