using AutoMapper;
using BarberFlow.Communication.Requests;
using BarberFlow.Communication.Responses;
using BarberFlow.Domain.Entities;

namespace BarberFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterBillingJson, Billing>();
    }

    private void EntityToResponse()
    {
        CreateMap<Billing, ResponseRegisterBillingJson>();
        CreateMap<Billing, ResponseListBillingsJson>();
        CreateMap<Billing, ResponseBillingJson>();
    }
}