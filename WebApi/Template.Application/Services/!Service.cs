using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.DTOs.User;
using Template.Domain.Base;
using Template.Domain.DataModels;
using Template.Domain.UnitOfWork;

namespace Template.Application.Services
{
    public partial class Service(IUnitOfWork unitOfWork, ILogger<Service> logger, IConfiguration configuration, IMediator mediator, IHttpContextAccessor accessor) : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<Service> _logger = logger;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMediator _mediator = mediator;
        private readonly IHttpContextAccessor _httpContextAccessor = accessor;
        private bool Disposed;


        private async Task DispatchDomainEventsAsync<T>(T entity) where T : BaseEntity<int>
        {
            var domainEvents = entity.GetDomainEvents().ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }

     
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                // dispose managed objects
            }

            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
