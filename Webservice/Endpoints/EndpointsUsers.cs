using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Authentication;
using AutoMapper;
using Contracts.DTO;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Webservice.Modules
{
    public static class EndpointsExtensions
    {
        public static void UserEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup($"/users");

            endpoints.MapGet("/getall", GetAll).RequireAuthorization("AdminAccess");
            endpoints.MapGet("/{id}", Get).RequireAuthorization("AdminAccess");
            endpoints.MapPatch("/{id}", Patch).RequireAuthorization("AdminAccess");
            endpoints.MapDelete("/{id}", Delete).RequireAuthorization("AdminAccess");
            endpoints.MapPost("/login", Login);
            endpoints.MapPost("/register", Register);
        }

        public static async Task<IResult> GetAll([FromServices] UserManager<ApplicationUser> _repo, IMapper _mapper)
        {
            List<ApplicationUser>? users = _repo.Users.ToList();
            return users is null ? TypedResults.NotFound("No users found") : TypedResults.Ok(_mapper.Map<List<UserDTO>>(users));
        }

        public static async Task<IResult> Get(Guid id, [FromServices] UserManager<ApplicationUser> _repo, IMapper _mapper)
        {
            ApplicationUser? user = await _repo.FindByIdAsync(id.ToString());
            return user is null ? TypedResults.NotFound($"No user with id {id}") : TypedResults.Ok(_mapper.Map<UserDTO>(user));
        }

        public static async Task<IResult> Patch(UserDTO user, [FromServices] UserManager<ApplicationUser> _repo, IValidator<UserDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(user);
            if (!result.IsValid)
                return TypedResults.BadRequest($"Failed to validate: {result.Errors}");

            IdentityResult identityResult = await _repo.UpdateAsync(_mapper.Map<ApplicationUser>(user));

            return identityResult.Succeeded is false ? TypedResults.BadRequest("Failed to update entity") : TypedResults.Ok(user);
        }

        public static async Task<IResult> Delete(Guid id, [FromServices] UserManager<ApplicationUser> _repo)
        {
            ApplicationUser? user = await _repo.FindByIdAsync(id.ToString());
            if (user is null)
                return Results.NotFound($"User with id {id} was not found");

            var result = await _repo.DeleteAsync(user);
            return result.Succeeded is false ? TypedResults.BadRequest() : TypedResults.NoContent();
        }

        public static async Task<IResult> Login(LoginUserDTO userLogin, [FromServices] UserManager<ApplicationUser> _repo, ITokenService _service)
        {
            ApplicationUser? user = await _repo.FindByEmailAsync(userLogin.Email);

            if (!await _repo.CheckPasswordAsync(user, userLogin.Password))
            {
                return Results.Unauthorized();
            }
            else if (user is null)
            {
                return Results.NotFound();
            }

            string token = await _service.GenerateTokenAsync(user);

            return Results.Ok(token);
        }

        public static async Task<IResult> Register(CreateUserDTO userRegister, [FromServices] UserManager<ApplicationUser> _repo, ITokenService _service, IValidator<CreateUserDTO> _validator)
        {
            ValidationResult result = _validator.Validate(userRegister);
            if (!result.IsValid)
                return Results.BadRequest("Invalid user");



            if (await _repo.FindByEmailAsync(userRegister.Email) is not null)
                return Results.BadRequest("That email already exists");

            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email
            };

            var identityResult = await _repo.CreateAsync(user, userRegister.Password);
            if (!identityResult.Succeeded)
                return Results.Problem();

            await _repo.AddToRoleAsync(user, "Customer");

            string token = await _service.GenerateTokenAsync(user);
            if (token == null)
            {
                return Results.BadRequest("Failed to create accesstoken");
            }

            return Results.Ok(new { Token = token });
        }

    }
}
