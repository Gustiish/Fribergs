using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Repository;
using AutoMapper;
using Contracts.DTO;
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

        public static async Task<IResult> Post(Car car, [FromServices] ICarRepository _repo)
        {
            return await _repo.CreateAsync(car) is false ? TypedResults.BadRequest() : TypedResults.Created();
        }

        public static async Task<IResult> Patch(Car car, [FromServices] ICarRepository _repo)
        {
            return await _repo.UpdateAsync(car) is false ? TypedResults.BadRequest() : TypedResults.Ok(car);
        }

        public static async Task<IResult> Delete(Guid id, [FromServices] ICarRepository _repo)
        {
            return await _repo.DeleteAsync(id) is false ? TypedResults.BadRequest() : TypedResults.NoContent();
        }
    }
}
