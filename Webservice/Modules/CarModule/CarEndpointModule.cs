using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repository;
using AutoMapper;
using Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Webservice.Modules.CarModule
{
    public static class CarEndpointModule
    {
        public static void CarEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/cars");

            endpoints.MapGet("/getall", GetAll);
            endpoints.MapGet("/{id}", Get);
            endpoints.MapPost("/", Post);
            endpoints.MapPatch("/{id}", Patch);
            endpoints.MapDelete("/{id}", Delete);
            endpoints.MapGet("/carreferences", GetCarReferences)
        }

        public static async Task<IResult> GetAll([FromServices] ICarRepository _repo, IMapper _mapper)
        {
            IEnumerable<Car>? cars = await _repo.GetAllAsync();
            List<CarDTO> carDTO = _mapper.Map<List<CarDTO>>(cars);
            return cars is null ? TypedResults.NotFound("No cars found") : TypedResults.Ok(carDTO);
        }

        public static async Task<IResult> Get(Guid id, [FromServices] ICarRepository _repo)
        {
            Car? car = _repo.Find(id);
            return car is null ? TypedResults.NotFound($"No car with id {id}") : TypedResults.Ok(car);
        }

        public static async Task<IResult> Post(CarDTO car, [FromServices] ICarRepository _repo, IValidator<CarDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
                return TypedResults.BadRequest($"Failed to validate: {result.Errors}");
            return await _repo.CreateAsync(_mapper.Map<Car>(car)) is false ? TypedResults.BadRequest($"Failed to create entity") : TypedResults.Created();
        }

        public static async Task<IResult> Patch(CarDTO car, [FromServices] ICarRepository _repo, IValidator<CarDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
                return TypedResults.BadRequest($"Failed to validate: {result.Errors}");
            return await _repo.UpdateAsync(_mapper.Map<Car>(car)) is false ? TypedResults.BadRequest("Failed to update entity") : TypedResults.Ok(car);
        }

        public static async Task<IResult> Delete(Guid id, [FromServices] ICarRepository _repo)
        {
            return await _repo.DeleteAsync(id) is false ? TypedResults.BadRequest() : TypedResults.NoContent();
        }

        public static async Task<IResult> GetCarReferences([FromServices] ICarReference _reference, IMapper _mapper)
        {
            var carReferences = _mapper.Map<List<BrandDTO>>(await _reference.GetCarReferencesAsync());
            return carReferences is null ? TypedResults.NotFound("No car references found") : TypedResults.Ok(carReferences);
        }
    }
}
