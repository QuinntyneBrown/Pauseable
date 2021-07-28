using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Pauseable.Api.Models;
using Pauseable.Api.Core;
using Pauseable.Api.Interfaces;

namespace Pauseable.Api.Features
{
    public class RemoveNotification
    {
        public class Request: IRequest<Response>
        {
            public Guid NotificationId { get; set; }
        }

        public class Response: ResponseBase
        {
            public NotificationDto Notification { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPauseableDbContext _context;
        
            public Handler(IPauseableDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var notification = await _context.Notifications.SingleAsync(x => x.NotificationId == request.NotificationId);
                
                _context.Notifications.Remove(notification);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Notification = notification.ToDto()
                };
            }
            
        }
    }
}
