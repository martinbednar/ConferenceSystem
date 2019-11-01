using AutoMapper;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.BL
{
    public static class MapperInitializer
    {
        public static bool IsInicialized { get; set; }

        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Organization, OrganizationDTO>();
                cfg.CreateMap<OrganizationDTO, Organization>()
                    .ForMember(dest => dest.Users, opt => opt.Ignore());
                cfg.CreateMap<AppUser, AppUserDTO>()
                    .ForMember(dest => dest.SequenceNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Organization))
                    .ForMember(dest => dest.Cartering, opt => opt.Ignore())
                    .ForMember(dest => dest.Workshops, opt => opt.Ignore())
                    .ForMember(dest => dest.Invoice, opt => opt.Ignore())
                    .ForMember(dest => dest.LecturerInfo, opt => opt.MapFrom(src => src.LecturerInfo));
                cfg.CreateMap<AppUserDTO, AppUser>()
                    .ForMember(dest => dest.WasEmailCarteringSent, opt => opt.Ignore())
                    .ForMember(dest => dest.WasEmailWorkshopSent, opt => opt.Ignore())
                    .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEndDateUtc, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                    .ForMember(dest => dest.Roles, opt => opt.Ignore())
                    .ForMember(dest => dest.Claims, opt => opt.Ignore())
                    .ForMember(dest => dest.Logins, opt => opt.Ignore())
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Invoice, opt => opt.Ignore())
                    .ForMember(dest => dest.LecturerInfo, opt => opt.MapFrom(src => src.LecturerInfo));
                cfg.CreateMap<InvoiceDTO, Invoice>();
                cfg.CreateMap<Invoice, InvoiceDTO>();
                cfg.CreateMap<TextDTO, Text>();
                cfg.CreateMap<Text, TextDTO>();
                cfg.CreateMap<ConstantDTO, Constant>();
                cfg.CreateMap<Constant, ConstantDTO>();
                cfg.CreateMap<AppUserRole, AppUserRoleDTO>();
                cfg.CreateMap<AppUserRoleDTO, AppUserRole>();
                cfg.CreateMap<LecturerInfoDTO, LecturerInfo>();
                cfg.CreateMap<LecturerInfo, LecturerInfoDTO>();
            });
        }
    }
}