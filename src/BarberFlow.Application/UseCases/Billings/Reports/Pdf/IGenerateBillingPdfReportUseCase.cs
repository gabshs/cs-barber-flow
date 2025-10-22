using System;

namespace BarberFlow.Application.UseCases.Billings.Reports.Pdf;

public interface IGenerateBillingPdfReportUseCase
{
    Task<byte[]> ExecuteAsync(DateOnly month);
}
