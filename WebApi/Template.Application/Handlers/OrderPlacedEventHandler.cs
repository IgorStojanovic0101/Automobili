using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Events;

namespace Template.Application.Handlers
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(ILogger<OrderPlacedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Order {notification.OrderId} was placed on {notification.OrderDate}.");
            // Additional logic (e.g., send email, etc.)
            return Task.CompletedTask;
        }
    }
}
