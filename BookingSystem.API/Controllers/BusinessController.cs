using BookingSystem.Application.Common;
using BookingSystem.Application.DTOs.Business;
using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/business")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpPost]
        [Authorize(Roles = "BusinessOwner")]
        public async Task<IActionResult> Create(CreateBusinessDto dto)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized(ApiResponse<int>.Fail(
                    "Unauthorized",
                    "غير مصرح"
                ));
            }

            var result = await _businessService.CreateAsync(dto, ownerId!);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<int>.Fail(
                    result.ErrorEn!,
                    result.ErrorAr!
                ));
            }

            return Ok(ApiResponse<int>.Ok(
            result.Data!,
            "Business created successfully",
            "تم إنشاء النشاط التجاري بنجاح"
            ));
        }
    }
}
