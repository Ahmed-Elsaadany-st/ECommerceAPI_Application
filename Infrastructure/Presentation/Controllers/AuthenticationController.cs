using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servieces.Abstractions;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiecManager _serviecManager) : ApiBaseController
    {
        #region login
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var result=await _serviecManager.authenticationService.LoginAsync(loginDto);
            return Ok(result);
        }
        #endregion
        #region register
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            var result = await _serviecManager.authenticationService.RegisterAsync(registerDto);
            return Ok(result);
        }
        #endregion
        #region Check Email

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result= await _serviecManager.authenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }
        #endregion
        #region CurrentUser
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserResultDto>> CurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var aapUser= await _serviecManager.authenticationService.GetCurrentUserAsync(Email!);
            return Ok(aapUser);
        }
        #endregion
        #region GetCurrentAddress
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviecManager.authenticationService.GetCurrentUserAddressAsync(Email!);
            return Ok(Address);

        }
        #endregion
        #region UpdateUserAddress
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var result= await _serviecManager.authenticationService.UpdateCurrentUserAddressAsync(Email!, address);
            return Ok(result);
        }
        #endregion



    }
}
