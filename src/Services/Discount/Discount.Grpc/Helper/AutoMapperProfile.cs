using AutoMapper;
using Discount.Grpc.Model.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
