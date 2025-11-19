using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Repository;
using AutoMapper;
using Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
            return cars is null ? TypedResults.NotFound("No cars found") : TypedResults.Ok(carDTO);
        }

        public static async Task<IResult> Get(Guid id, [FromServices] IRepository<Car> _repo, IMapper _mapper)
        {
            Car? car = await _repo.FindAsync(id);
            return car is null ? TypedResults.NotFound($"No car with id {id}") : TypedResults.Ok(_mapper.Map<CarDTO>(car));
        }

        public static async Task<IResult> Post(CreateCarDTO car, [FromServices] IRepository<Car> _repo, IValidator<CreateCarDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
                return TypedResults.BadRequest($"Failed to validate: {result.Errors}");

            return await _repo.CreateAsync(_mapper.Map<Car>(car)) is false
              ? TypedResults.BadRequest("Failed to create entity")
              : TypedResults.Created();
        }

        public static async Task<IResult> Patch(CarDTO car, [FromServices] IRepository<Car> _repo, IValidator<CarDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
                return TypedResults.BadRequest($"Failed to validate: {result.Errors}");
            return await _repo.UpdateAsync(_mapper.Map<Car>(car)) is false ? TypedResults.BadRequest("Failed to update entity") : TypedResults.Ok(car);
        }

        public static async Task<IResult> Delete(Guid id, [FromServices] IRepository<Car> _repo)
        {
            return await _repo.DeleteAsync(id) is false ? TypedResults.BadRequest() : TypedResults.NoContent();
        }


    }
}
