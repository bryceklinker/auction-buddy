using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Harvest.Home.Core.Auctions.Create;
using Harvest.Home.Core.Common.Serialization;
using Harvest.Home.Core.Common.Storage;
using Harvest.Home.Core.Common.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Harvest.Home.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuctionBuddy(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAutoMapper(typeof(ServiceCollectionExtensions))
                .AddTransient<IValidator<CreateAuctionDto>, CreateAuctionValidator>()
                .AddTransient<IUseCase<CreateAuctionDto, ValidationResult>, CreateAuctionUseCase>()
                .AddSingleton<IAuctionBuddySerializer, AuctionBuddySerializer>()
                .AddDbContext<StorageContext>(opts =>
                {
                    opts.UseInMemoryDatabase("memory");
                })
                .AddTransient<IStorage, Storage>();
        }
    }
}