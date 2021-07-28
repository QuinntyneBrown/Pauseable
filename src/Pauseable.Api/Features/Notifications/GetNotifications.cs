using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Pauseable.Api.Core;
using Pauseable.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Pauseable.Api.Features
{
    public class GetNotifications
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<NotificationDto> Notifications { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPauseableDbContext _context;
        
            public Handler(IPauseableDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Notifications = await _context.Notifications.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
