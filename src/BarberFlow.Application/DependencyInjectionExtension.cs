using BarberFlow.Application.UseCases.Billings.Delete;
using BarberFlow.Application.UseCases.Billings.GetById;
using BarberFlow.Application.UseCases.Billings.ListAll;
using BarberFlow.Application.UseCases.Billings.Register;
using BarberFlow.Application.UseCases.Billings.Reports.Excel;
using BarberFlow.Application.UseCases.Billings.Reports.Pdf;
using BarberFlow.Application.UseCases.Billings.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberFlow.Application;

public static class DependencyInjectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapper.AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IListAllBillingsUseCase, ListAllBillingsUseCase>();
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
        services.AddScoped<IGetBillingByIdUseCase, GetBillingByIdUseCase>();
        services.AddScoped<IUpdateBillingUseCase, UpdateBillingUseCase>();
        services.AddScoped<IDeleteBillingUseCase, DeleteBillingUseCase>();
        services.AddScoped<IGenerateBillingExcelReportUseCase, GenerateBillingExcelReportUseCase>();
        services.AddScoped<IGenerateBillingPdfReportUseCase, GenerateBillingPdfReportUseCase>();

    }
}