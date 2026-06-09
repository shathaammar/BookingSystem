using BookingSystem.Application.Common;
using BookingSystem.Application.DTOs.Auth;
using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDto dto)
        {
            var result = await _authService.RegisterCustomerAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail(result.ErrorEn!, result.ErrorAr!));

            var msg = ResponseMessages.RegisterSuccess;
            return Ok(ApiResponse<AuthResponseDto>.Ok(result.Data!, msg.En, msg.Ar));
        }

        [HttpPost("register/business-owner")]
        public async Task<IActionResult> RegisterBusinessOwner([FromBody] RegisterBusinessOwnerDto dto)
        {
            var result = await _authService.RegisterBusinessOwnerAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail(result.ErrorEn!, result.ErrorAr!));

            var msg = ResponseMessages.RegisterSuccess;
            return Ok(ApiResponse<AuthResponseDto>.Ok(result.Data!, msg.En, msg.Ar));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail(result.ErrorEn!, result.ErrorAr!));
            var msg = ResponseMessages.LoginSuccess;
            return Ok(ApiResponse<AuthResponseDto>.Ok(result.Data!, msg.En, msg.Ar));
        }
    }
}
