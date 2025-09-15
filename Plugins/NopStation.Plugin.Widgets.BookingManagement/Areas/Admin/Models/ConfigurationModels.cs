using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;

public partial record ConfigurationModel : BaseNopModel, ISettingsModel
{

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.Configuration.Fields.EnableBookingManagement")]
    public bool EnableBookingManagement { get; set; }
    public bool EnableBookingManagement_OverrideForStore { get; set; }
    public int ActiveStoreScopeConfiguration { get; set; }
}