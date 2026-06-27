using BookingSystem.Application.Common;
using BookingSystem.Application.DTOs.Auth;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Enums;
using BookingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager,
                           IConfiguration configuration,
                           AppDbContext context,
                           ITokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResponseDto>> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            return await RegisterAsync(
                dto.FullName,
                dto.Email,
                dto.PhoneNumber,
                dto.Password,
                UserRoles.Customer
            );
        }

        public async Task<Result<AuthResponseDto>> RegisterBusinessOwnerAsync(RegisterBusinessOwnerDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                var m = ResponseMessages.EmailAlreadyExists;
                return Result<AuthResponseDto>.Failure(m.En, m.Ar);
            }

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            // 1. CREATE USER (IMPORTANT)
            var createResult = await _userManager.CreateAsync(user, dto.Password);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ",
                    createResult.Errors.Select(x => x.Description));

                return Result<AuthResponseDto>.Failure(errors, errors);
            }

            // 2. ADD ROLE (AFTER USER CREATED)
            var roleResult = await _userManager.AddToRoleAsync(user, UserRoles.BusinessOwner);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ",
                    roleResult.Errors.Select(x => x.Description));

                return Result<AuthResponseDto>.Failure(errors, errors);
            }

            // 3. CREATE BUSINESS
            var business = new Business
            {
                Name = dto.BusinessName,
                BusinessOwnerId = user.Id
            };

            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();

            var token = await _tokenService.GenerateTokenAsync(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = UserRoles.BusinessOwner
            });
        }

        public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                var m = ResponseMessages.InvalidCredentials;
                return Result<AuthResponseDto>.Failure(m.En, m.Ar);
            }

            var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValid)
            {
                var m = ResponseMessages.InvalidCredentials;
                return Result<AuthResponseDto>.Failure(m.En, m.Ar);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty;

            var token = await _tokenService.GenerateTokenAsync(user);

            return Result<AuthResponseDto>.Success(
                new AuthResponseDto
                {
                    Token = token,
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Role = role
                }
            );
        }

        private async Task<Result<AuthResponseDto>> RegisterAsync(
                string fullName,
                string email,
                string phoneNumber,
                string password,
                string role)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                var m = ResponseMessages.EmailAlreadyExists;
                return Result<AuthResponseDto>.Failure(m.En, m.Ar);
            }

            var user = new ApplicationUser
            {
                FullName = fullName,
                Email = email,
                UserName = email,
                PhoneNumber = phoneNumber
            };

            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ",
                    createResult.Errors.Select(x => x.Description));

                return Result<AuthResponseDto>.Failure(errors, errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ",
                    roleResult.Errors.Select(x => x.Description));

                return Result<AuthResponseDto>.Failure(errors, errors);
            }

            var token = await _tokenService.GenerateTokenAsync(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = role
            });
        }
    }
}