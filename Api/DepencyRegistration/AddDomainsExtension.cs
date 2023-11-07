using Logic.Interfaces;
using Logic.Services;
using Api.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace Api.DepencyRegistration
{
    public static class AddDomainServices
    {
        public static void AddLogicServices(this IServiceCollection services)
        {
            services
                .AddTransient<IContactsService, ContactsService>()
                .AddTransient<IContragentsService, ContragentsService>()
                .AddTransient<GlobalExceptionHandlerMiddleware>();

            services.TryAddScoped<HttpClient>();
            services.TryAddScoped<WebClient>();
        }
    }
}

