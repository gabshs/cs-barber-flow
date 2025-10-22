using BarberFlow.Application.UseCases.Billings.Delete;
using BarberFlow.Application.UseCases.Billings.GetById;
using BarberFlow.Application.UseCases.Billings.ListAll;
using BarberFlow.Application.UseCases.Billings.Register;
using BarberFlow.Application.UseCases.Billings.Update;
using BarberFlow.Communication.Requests;
using BarberFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponsePaginatedJson<ResponseListBillingsJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAll([FromServices] IListAllBillingsUseCase useCase, [FromQuery] RequestListPaginatedBillingsJson request)
    {
        var response = await useCase.ExecuteAsync(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetBillingByIdUseCase useCase, [FromRoute] Guid id)
    {
        var response = await useCase.ExecuteAsync(id);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterBillingJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterBillingUseCase useCase, [FromBody] RequestRegisterBillingJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateBillingUseCase useCase, [FromRoute] Guid id, [FromBody] RequestRegisterBillingJson request)
    {
        await useCase.ExecuteAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteBillingUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }
}
