using Microsoft.AspNetCore.Mvc;
using Servieces.Abstractions;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
