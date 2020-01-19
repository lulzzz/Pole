﻿using Pole.Application.EventBus;
using Pole.Domain;
using Product.Api.Domain.Event;
using Product.Api.Domain.ProductAggregate;
using Product.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Api.Application.DomainEventHandler
{
    public class AddDefaultProductWhenProductTypeAdded2DomainEventHandler : IDomainEventHandler<ProductTypeAddedDomainEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventBus _eventBus;
        public AddDefaultProductWhenProductTypeAdded2DomainEventHandler(IProductRepository productRepository, IEventBus eventBus)
        {
            _productRepository = productRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(ProductTypeAddedDomainEvent request, CancellationToken cancellationToken)
        {
            Product.Api.Domain.ProductAggregate.Product product = new Product.Api.Domain.ProductAggregate.Product(Guid.NewGuid().ToString("N"), request.ProductTypeName, 100, request.ProductTypeId);
            _productRepository.Add(product);
            var backId = Guid.NewGuid().ToString("N");
            ProductAddedIntegrationEvent productAddedIntegrationEvent = new ProductAddedIntegrationEvent()
            {
                BacketId = backId,
                Price = product.Price,
                ProductName = product.Name,
                ProductId = product.Id
            };

            await _eventBus.Publish(productAddedIntegrationEvent, product.Id);
            await _productRepository.SaveEntitiesAsync();
        }
    }
}
