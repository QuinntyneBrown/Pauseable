using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Pauseable.Api.Core;
using Pauseable.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Pauseable.Api.Features
{
    public class UpdateNotification
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Notification).NotNull();
                RuleFor(request => request.Notification).SetValidator(new NotificationValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public NotificationDto Notification { get; set; }
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
                var notification = await _context.Notifications.SingleAsync(x => x.NotificationId == request.Notification.NotificationId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Notification = notification.ToDto()
                };
            }
            
        }
    }
}
