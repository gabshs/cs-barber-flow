namespace BarberFlow.Application.UseCases.Billings.Reports.Excel;

public interface IGenerateBillingExcelReportUseCase
{
    Task<byte[]> ExecuteAsync(DateOnly month);

}
