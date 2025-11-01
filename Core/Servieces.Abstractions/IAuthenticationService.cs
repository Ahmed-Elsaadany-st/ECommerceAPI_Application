using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Abstractions
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<bool>CheckEmailAsync(string Email);
        Task<AddressDto>GetCurrentUserAddressAsync(string Email);
        Task<AddressDto>UpdateCurrentUserAddressAsync(string Email,AddressDto addressDto);
        Task<UserResultDto>GetCurrentUserAsync(string Email);


    }
}
