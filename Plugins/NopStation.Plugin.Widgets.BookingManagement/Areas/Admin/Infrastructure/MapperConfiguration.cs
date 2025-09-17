using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models;
using NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Models.DailyBookingCapacity;
using NopStation.Plugin.Widgets.BookingManagement.Domains;

namespace NopStation.Plugin.Widgets.BookingManagement.Areas.Admin.Infrastructure;

public class MapperConfiguration : Profile, IOrderedMapperProfile
{
    public int Order => 1;

    public MapperConfiguration()
    {
        CreateMap<ConfigurationModel, BookingManagementSettings>().ReverseMap();
        CreateMap<BookingProduct, BookingProductModel>().ReverseMap();
        CreateMap<DailyBookingCapacity, DailyBookingCapacityModel>().ReverseMap();
    }
}
