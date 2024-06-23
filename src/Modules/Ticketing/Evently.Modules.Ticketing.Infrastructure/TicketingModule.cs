using Evently.Common.Infrastructure.Interceptors;
using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Application.Carts;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Infrastructure.Customers;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Evently.Modules.Ticketing.Presentation.Customers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Evently.Modules.Ticketing.Presentation;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Tickets;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.Infrastructure.Events;
using Evently.Modules.Ticketing.Infrastructure.Orders;
using Evently.Modules.Ticketing.Infrastructure.Payments;
using Evently.Modules.Ticketing.Infrastructure.Tickets;
using Evently.Modules.Ticketing.Application.Abstractions.Payments;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static void MapEndpoints(IEndpointRouteBuilder  app)
    {
        TicketingEndpoints.MapEndpoints(app);
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();
    }

    
    public static IServiceCollection AddTicketingModule(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<TicketingDbContext>((sp, options) => options
            .UseNpgsql(dbConnnectionString)
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

        services.AddSingleton<CartService>();
        services.AddSingleton<IPaymentService, PaymentService>();

        return services;
    }
}
