
using AutoMapper;

using WesternMutual_RhyssLeary.Repository;
using WesternMutual_RhyssLeary.Services;

namespace WesternMutual_RhyssLeary;

public static class AppConfig
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGuidService, GuidService>();
        services.AddAutoMapper(typeof(AutoMapper));
    }
    public static class Defaults
    {
        public static int ExpirationDays { get; set; }

        public static void SetExpirationDays(string days)
        {
            if (int.TryParse(days, out int defaultExpirationDays))
            {
                //ExpirationDays = DateTime.Now.AddDays(defaultExpirationDays);
                ExpirationDays = defaultExpirationDays;
            }
            else
                throw new Exception($"An invalid value was specified for appSettings[Defaults:ExpirationDays]: {days}");
        }
    }
    public static class AutoMapper
    {
        public class Entry : Profile
        {
            public Entry()
            {
                // Mapping from CreateModel to Domain.Entry is too cumbersome.
                //CreateMap<Models.Entry.CreateModel, Domain.Entry>(MemberList.Destination)
                //    .ForMember(d => d.Guid, o => o.MapFrom(s => s.Guid))
                //    .ForMember(d => d.Expire,
                //        o => o.MapFrom(s =>
                //            (s.Expire == null)
                //                ? DateTime.Now
                //                : ((long)s.Expire).ToDate()))
                //    .ForMember(d => d.User, o => o.MapFrom(s => s.User))
                //    ;

                CreateMap<Domain.Entry, Models.Entry.ViewModel>(MemberList.Destination)
                    .ForMember(d => d.Guid, o => o.MapFrom(s => s.Guid.ToString().ToUpper()))
                    .ForMember(d => d.Expire, o => o.MapFrom(s => ((DateTime)s.Expire).ToEpochDateTime()))
                    .ForMember(d => d.User, o => o.MapFrom(s => s.User))
                    ;
    
            }
        }
    }
}
