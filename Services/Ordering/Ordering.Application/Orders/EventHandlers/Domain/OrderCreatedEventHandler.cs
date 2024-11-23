﻿
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extentions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
	(IPublishEndpoint publishEndpoint,
	 IFeatureManager featureManager,
	 ILogger<OrderCreatedEventHandler> logger)
	: INotificationHandler<OrderCreatedEvent>
{
	public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
	{
		logger.LogInformation("Domain Event Handled{DomainEvent}",
							  domainEvent.GetType().Name);

		if(await featureManager.IsEnabledAsync("OrderFullfilment"))
		{
			var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
			await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
		}
	}
}
