using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Authentication;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Records;
using AutoMapper;
using Contracts.DTO;
using Contracts.Services;
using Contracts.Services.Authentication;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webservice.Services.Factories;

namespace Webservice.Endpoints
{
    public static class EndpointsUser
    {
        public static void UserEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup($"/users");

            endpoints.MapGet("/getall", GetAll).RequireAuthorization("AdminAccess");
            endpoints.MapGet("/{id}", Get).RequireAuthorization("AdminOrCustomer");
            endpoints.MapPatch("/{id}", Patch).RequireAuthorization("AdminAccess");
            endpoints.MapDelete("/{id}", Delete).RequireAuthorization("AdminAccess");
            endpoints.MapPost("/login", Login);
            endpoints.MapPost("/register", Register);
            endpoints.MapPost("/refresh", Refresh);
        }

        public static async Task<IResult> GetAll([FromServices] UserManager<ApplicationUser> _repo, IMapper _mapper)
        {
            List<ApplicationUser> users = _repo.Users.ToList();
            if (users == null || users.Count == 0)
            {
                return Results.NotFound(ApiResponseFactory<List<UserDTO>>.CreateResponse(false, 404, null, "Users is null or empty"));
            }

            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (ApplicationUser user in users)
            {
                var roles = await _repo.GetRolesAsync(user);

                var dto = _mapper.Map<UserDTO>(user);
                dto.Roles = roles.ToArray();
                userDTOs.Add(dto);
            }
            return Results.Ok(ApiResponseFactory<List<UserDTO>>.CreateResponse(true, 200, userDTOs, "Success"));
        }

        public static async Task<IResult> Get(Guid id, [FromServices] UserManager<ApplicationUser> _repo, IMapper _mapper, IRepository<CustomerOrder> _repoOrder)
        {
            ApplicationUser? user = await _repo.FindByIdAsync(id.ToString());

            if (user is null)
                return Results.NotFound(ApiResponseFactory<UserDTO>.CreateResponse(false, 404, null, $"User with id {id} was not found"));

            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            var roles = await _repo.GetRolesAsync(user);
            userDTO.Roles = roles.ToArray();
            userDTO.Orders = _mapper.Map<List<OrderDTO>>(await _repoOrder.GetAllAsync());

            return Results.Ok(ApiResponseFactory<UserDTO>.CreateResponse(true, 200, userDTO, "Success"));
        }


        public static async Task<IResult> Patch(UserDTO user, [FromServices] UserManager<ApplicationUser> _repo, IValidator<UserDTO> _validator, IMapper _mapper)
        {
            var result = _validator.Validate(user);

            if (!result.IsValid)
                return Results.BadRequest(ApiResponseFactory<UserDTO>.CreateResponse(false, 400, user, $"Failed to validate: {result.Errors}"));

            IdentityResult identityResult = await _repo.UpdateAsync(_mapper.Map<ApplicationUser>(user));

            if (!identityResult.Succeeded)
                return Results.BadRequest(ApiResponseFactory<UserDTO>.CreateResponse(false, 400, user, "Failed to update entity"));

            return Results.Ok(ApiResponseFactory<UserDTO>.CreateResponse(true, 200, user, "Success"));
        }


        public static async Task<IResult> Delete(Guid id, [FromServices] UserManager<ApplicationUser> _repo)
        {
            ApplicationUser? user = await _repo.FindByIdAsync(id.ToString());

            if (user is null)
                return Results.NotFound(ApiResponseFactory<UserDTO>.CreateResponse(false, 404, null, $"User with id {id} was not found"));

            IdentityResult result = await _repo.DeleteAsync(user);

            if (!result.Succeeded)
                return Results.BadRequest(ApiResponseFactory<UserDTO>.CreateResponse(false, 400, null, "Failed to delete user"));

            return Results.Ok(ApiResponseFactory<UserDTO>.CreateResponse(true, 200, null, "Success"));
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

            TokenPair tokens = await _service.GenerateInitalTokenAsync(user);
            return Results.Ok(new TokenResponse(tokens.AccessToken, tokens.RefreshToken));
        }

        public static async Task<IResult> Register(CreateUserDTO userRegister, [FromServices] UserManager<ApplicationUser> _repo, ITokenService _service, IValidator<CreateUserDTO> _validator)
        {
            ValidationResult result = _validator.Validate(userRegister);
            if (!result.IsValid)
                return Results.BadRequest();



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

            TokenPair tokens = await _service.GenerateInitalTokenAsync(user);


            return Results.Ok(new TokenResponse(tokens.AccessToken, tokens.RefreshToken));
        }

        public static async Task<IResult> Refresh(RefreshTokenRequest request, [FromServices] ITokenService _service)
        {
            TokenPair tokens = await _service.RotateTokenAsync(request.RefreshToken);
            if (tokens == null)
                return Results.BadRequest("Failed to create refresh and access tokens or tokens have been expired.");

            return Results.Ok(new TokenResponse(tokens.AccessToken, tokens.RefreshToken));
        }

    }
}


