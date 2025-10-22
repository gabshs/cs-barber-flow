using System.Net.Mime;
using BarberFlow.Application.UseCases.Billings.Reports.Excel;
using BarberFlow.Application.UseCases.Billings.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcelReport([FromServices] IGenerateBillingExcelReportUseCase useCase, [FromHeader] DateOnly month)
    {
        byte[] file = await useCase.ExecuteAsync(month);


        if (file == null || file.Length < 1)
        {
            return NoContent();
        }

        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }

    [HttpGet("pdf")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdfReport([FromServices] IGenerateBillingPdfReportUseCase useCase, [FromHeader] DateOnly month)
    {
        byte[] file = await useCase.ExecuteAsync(month);

        if (file == null || file.Length < 1)
        {
            return NoContent();
        }

        return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
    }
}
