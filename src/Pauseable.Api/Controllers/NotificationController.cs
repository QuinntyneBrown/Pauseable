using System.Net;
using System.Threading.Tasks;
using Pauseable.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Pauseable.Api.Services;

namespace Pauseable.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController: Controller
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        public NotificationController(IMediator mediator, INotificationService notificationService)
        {
            _mediator = mediator;
            _notificationService = notificationService;

        }

        [HttpGet("queue")]
        public async Task Queue(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            _notificationService.Subscribe(async e =>
            {
                var orders = JsonConvert.SerializeObject(e);

                await response
                .WriteAsync($"data: {orders}\r\r");

                response.Body.Flush();

            });

            await tcs.Task;

        }

        [HttpGet("{notificationId}", Name = "GetNotificationByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetNotificationById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetNotificationById.Response>> GetById([FromRoute]GetNotificationById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.Notification == null)
            {
                return new NotFoundObjectResult(request.NotificationId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetNotificationsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetNotifications.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetNotifications.Response>> Get()
            => await _mediator.Send(new GetNotifications.Request());
        
        [HttpPost(Name = "CreateNotificationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateNotification.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateNotification.Response>> Create([FromBody]CreateNotification.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetNotificationsPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetNotificationsPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetNotificationsPage.Response>> Page([FromRoute]GetNotificationsPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateNotificationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateNotification.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateNotification.Response>> Update([FromBody]UpdateNotification.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{notificationId}", Name = "RemoveNotificationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveNotification.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveNotification.Response>> Remove([FromRoute]RemoveNotification.Request request)
            => await _mediator.Send(request);
        
    }
}
