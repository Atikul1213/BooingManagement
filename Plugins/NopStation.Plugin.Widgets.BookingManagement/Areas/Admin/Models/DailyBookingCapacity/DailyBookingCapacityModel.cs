using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models.DailyBookingCapacity;
public record class DailyBookingCapacityModel : BaseNopEntityModel
{
    public DailyBookingCapacityModel()
    {
        DailyBookingProducts = new List<SelectListItem>();
    }
    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityConfigurationModel.Fields.ProductId")]
    public int ProductId { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day1Capacity")]
    public int Day1Capacity { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day2Capacity")]
    public int Day2Capacity { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day3Capacity")]
    public int Day3Capacity { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day4Capacity")]
    public int Day4Capacity { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day5Capacity")]
    public int Day5Capacity { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day6Capacity")]
    public int Day6Capacity { get; set; }

    [NopResourceDisplayName("Admin.NopStation.BookingManagement.DailyBookingCapacityModel.Fields.Day7Capacity")]
    public int Day7Capacity { get; set; }
    public IList<SelectListItem> DailyBookingProducts { get; set; }

}
