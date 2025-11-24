using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Repository;
using AutoMapper;
using Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webservice.Services.Factories;

namespace Webservice.Endpoints
{
    public static class EndpointsOrder
    {
        public static void OrderEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/orders");

            endpoints.MapGet("/getall", GetAll).RequireAuthorization("AdminOrCustomer");
            endpoints.MapGet("/{id}", Get).RequireAuthorization("AdminOrCustomer");
            endpoints.MapPost("/", Post).RequireAuthorization("AdminOrCustomer");
            endpoints.MapPatch("/{id}", Patch).RequireAuthorization("AdminAccess");
            endpoints.MapDelete("/{id}", Delete).RequireAuthorization("AdminAccess");
        }

        public static async Task<IResult> GetAll([FromServices] IRepository<CustomerOrder> _repo, IMapper _mapper)
        {
            List<CustomerOrder>? orders = await _repo.Query().Include(o => o.Car).ToListAsync();
            List<OrderDTO> orderDTO = _mapper.Map<List<OrderDTO>>(orders);

            if (orders is null)
            {
                return Results.NotFound(ApiResponseFactory<List<OrderDTO>>.CreateResponse(false, 404, orderDTO, "Orders is null"));
            }
            else
            {
                return Results.NotFound(ApiResponseFactory<List<OrderDTO>>.CreateResponse(true, 200, orderDTO, "Success"));
            }
        }

        public static async Task<IResult> Get(Guid id, [FromServices] IRepository<CustomerOrder> _repo, IMapper _mapper)
        {
            CustomerOrder order = await _repo.Query().Include(o => o.Car).FirstOrDefaultAsync(o => o.Id == id);
            OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);

            if (order is null)
            {
                return Results.NotFound(ApiResponseFactory<OrderDTO>.CreateResponse(false, 404, orderDTO, "Order is null"));
            }
            else
            {
                return Results.NotFound(ApiResponseFactory<OrderDTO>.CreateResponse(true, 200, orderDTO, "Success"));
            }
        }

        public static async Task<IResult> Post(CreateOrderDTO createOrderDTO, [FromServices] IRepository<CustomerOrder> _repo, IMapper _mapper, IValidator<CreateOrderDTO> _validator)
        {
            var result = _validator.Validate(createOrderDTO);
            if (!result.IsValid)
            {
                return Results.BadRequest(ApiResponseFactory<CreateOrderDTO>.CreateResponse(false, 400, null, "Failed to validate"));
            }
            else if (!await _repo.CreateAsync(_mapper.Map<CustomerOrder>(createOrderDTO)))
            {
                return Results.Json(ApiResponseFactory<CreateOrderDTO>.CreateResponse(false, 500, createOrderDTO, "Failed to create"), contentType: "application/json", statusCode: 500);
            }
            else
            {
                return Results.Ok(ApiResponseFactory<CreateOrderDTO>.CreateResponse(true, 200, createOrderDTO, "Success"));
            }
        }

        public static async Task<IResult> Patch(OrderDTO orderDTO, [FromServices] IRepository<CustomerOrder> _repo, IMapper _mapper, IValidator<OrderDTO> _validator)
        {
            var result = _validator.Validate(orderDTO);
            if (!result.IsValid)
            {
                return Results.BadRequest(ApiResponseFactory<OrderDTO>.CreateResponse(false, 400, null, "Failed to validate"));
            }
            else if (!await _repo.UpdateAsync(_mapper.Map<CustomerOrder>(orderDTO)))
            {
                return Results.Json(ApiResponseFactory<OrderDTO>.CreateResponse(false, 500, orderDTO, "Failed to update"), contentType: "application/json", statusCode: 500);
            }
            else
            {
                return Results.Ok(ApiResponseFactory<OrderDTO>.CreateResponse(true, 200, orderDTO, "Success"));
            }
        }

        public static async Task<IResult> Delete(Guid id, [FromServices] IRepository<CustomerOrder> _repo)
        {
            if (!await _repo.DeleteAsync(id))
            {
                return Results.Json(ApiResponseFactory<OrderDTO>.CreateResponse(false, 500, null, "Failed to delete"), contentType: "application/json", statusCode: 500);
            }
            else
            {
                return Results.NoContent();
            }
        }
    }
}
