using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Repository;
using AutoMapper;
using Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Webservice.Services.Factories;

namespace Webservice.Modules.CarModule
{
    public static class EndpointsExtensions
    {
        public static void CarEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/cars");

            endpoints.MapGet("/getall", GetAll);
            endpoints.MapGet("/{id}", Get);
            endpoints.MapPost("/", Post).RequireAuthorization("AdminAccess");
            endpoints.MapPatch("/{id}", Patch).RequireAuthorization("AdminAccess");
            endpoints.MapDelete("/{id}", Delete).RequireAuthorization("AdminAccess");
        }

        public static async Task<IResult> GetAll([FromServices] IRepository<Car> _repo, IMapper _mapper)
        {
            IEnumerable<Car>? cars = await _repo.GetAllAsync();
            List<CarDTO> carDTO = _mapper.Map<List<CarDTO>>(cars);

            if (cars is null)
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(false, 404, null, "Cars is null"), statusCode: 404);
            }
            else
            {
                return Results.Json(ApiResponseFactory<List<CarDTO>>.CreateResponse(true, 200, _mapper.Map<List<CarDTO>>(cars), "Success"), statusCode: 200);
            }

        }

        public static async Task<IResult> Get(Guid id, [FromServices] IRepository<Car> _repo, IMapper _mapper)
        {
            Car? car = await _repo.FindAsync(id);

            if (car == null)
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(false, 404, null, "Failed to find car"), statusCode: 404);
            }
            else
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(true, 200, _mapper.Map<CarDTO>(car), "Success"), statusCode: 200);
            }
        }

        public static async Task<IResult> Post(CreateCarDTO car, [FromServices] IRepository<Car> _repo, IValidator<CreateCarDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
            {

                return Results.Json(ApiResponseFactory<CreateCarDTO>.CreateResponse(false, 400, car, "Failed to validate"), statusCode: 400);
            }
            else if (!await _repo.CreateAsync(_mapper.Map<Car>(car)))
            {
                return Results.Json(ApiResponseFactory<CreateCarDTO>.CreateResponse(false, 500, car, "Failed to create"), statusCode: 500);
            }
            else
            {
                return Results.Json(ApiResponseFactory<CreateCarDTO>.CreateResponse(true, 201, car, "Success"), statusCode: 201);
            }
        }

        public static async Task<IResult> Patch(CarDTO car, [FromServices] IRepository<Car> _repo, IValidator<CarDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(false, 400, car, "Failed to validate"), statusCode: 400);
            }
            else if (!await _repo.UpdateAsync(_mapper.Map<Car>(car)))
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(false, 500, car, "Failed to update"), statusCode: 500);
            }
            else
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(true, 200, null, "Success"), statusCode: 200);
            }
        }

        public static async Task<IResult> Delete(Guid id, [FromServices] IRepository<Car> _repo)
        {
            if (!await _repo.DeleteAsync(id))
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(false, 500, null, "Failed to delete"), statusCode: 500);
            }
            else
            {
                return Results.Json(ApiResponseFactory<CarDTO>.CreateResponse(true, 204, null, "Success"), statusCode: 204);
            }


        }


    }
}
