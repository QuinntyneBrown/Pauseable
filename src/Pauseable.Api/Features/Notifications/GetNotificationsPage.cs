using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Pauseable.Api.Extensions;
using Pauseable.Api.Core;
using Pauseable.Api.Interfaces;
using Pauseable.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Pauseable.Api.Features
{
    public class GetNotificationsPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<NotificationDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPauseableDbContext _context;
        
            public Handler(IPauseableDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from notification in _context.Notifications
                    select notification;
                
                var length = await _context.Notifications.CountAsync();
                
                var notifications = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = notifications
                };
            }
            
        }
    }
}
