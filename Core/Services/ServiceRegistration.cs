using Core.Repositories;
using Core.UnitOfWorks;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfraStructer(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IBookFoodRepository, BookFoodRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IPurchaseRespository, PurchaseRepository>();
            services.AddTransient<IMailContentRepo, MailContentRepo>();
            services.AddTransient<IMailSettingRepo, MailSettingRepo>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IMailService, MailService>();

        }
    }
}
