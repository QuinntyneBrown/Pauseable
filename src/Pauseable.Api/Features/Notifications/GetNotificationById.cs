using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Pauseable.Api.Core;
using Pauseable.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Pauseable.Api.Features
{
    public class GetNotificationById
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
                return new () {
                    Notification = (await _context.Notifications.SingleOrDefaultAsync(x => x.NotificationId == request.NotificationId)).ToDto()
                };
            }
            
        }
    }
}
