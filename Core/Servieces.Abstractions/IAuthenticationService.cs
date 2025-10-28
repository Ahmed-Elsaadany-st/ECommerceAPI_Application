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
        //Login    =>Returns UserResultDto [Email-DisplayName-Token]  Takes==> [Email-Password]
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //Register =>Returns UserResultDto [Email-DisplayName-Token]  Takes==> [Email-Password-PhoneNumber-UserName-DisplayName]
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
