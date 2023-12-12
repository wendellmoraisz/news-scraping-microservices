using System.Net;
using EmailManager.Application.UseCases.CreateEmailAddress;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailManager.API.Controllers;

public class EmailController : ApiController
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("CreateEmail")]
    [ProducesResponseType(typeof(CreateEmailAddressResponse), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<CreateEmailAddressResponse>> CreateEmail([FromBody] CreateEmailAddressRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Created("GetEmailByAddress", new { id = response.Id, address = response.Address });
    }
}