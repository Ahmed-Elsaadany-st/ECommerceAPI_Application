using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Servieces.Abstractions;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class AuthenticationService(UserManager<ApplicationUser>_userManager, IConfiguration configuraion,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserResultDto(User.DisplayName,User.Email, await CreateTokenAsync(User));
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(u => u.Address)
               .FirstOrDefaultAsync(u => u.Email == Email)
               ?? throw new UserNotFoundException(Email);

            if (User.Address is not null)
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;
              
            }
            else
            {
                User!.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
             
            await _userManager.UpdateAsync(User); //Saved or not?

            return _mapper.Map<AddressDto>(User.Address);

        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var User = await _userManager.Users.Include(u => u.Address)
                 .FirstOrDefaultAsync(u => u.Email == Email)
                 ?? throw new UserNotFoundException(Email);


            if (User.Address is not null)
            
                return _mapper.Map<Address, AddressDto>(User.Address);
            

                else throw new AddressNotFoundException(User!.UserName!);


        }


        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is not null)
            {
                bool isEqual= await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (isEqual) return new UserResultDto(user.DisplayName, user.Email,await CreateTokenAsync(user));
                else throw new UnAuthrizedException();

            }
            throw new UserNotFoundException(loginDto.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
           var appUser = new ApplicationUser()
           {
               DisplayName=registerDto.DisplayName,
               Email=registerDto.Email,
               UserName=registerDto.DisplayName,
               PhoneNumber=registerDto.PhoneNumber,
           };
            var Result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (Result.Succeeded)
            {
                return new UserResultDto(registerDto.DisplayName, registerDto.Email,await CreateTokenAsync(appUser));
            }
            else
            {
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private  async Task <string> CreateTokenAsync(ApplicationUser user)
        {
           var Claims = new List<Claim>()
           {
               new (ClaimTypes.Email,user.Email!),
               new (ClaimTypes.Name,user.UserName!),
               new (ClaimTypes.NameIdentifier,user.Id)
           };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) 
                Claims.Add(new Claim(ClaimTypes.Role, role));

            //-------------------------
            var SecretKey = configuraion.GetSection("JWToptions")["SecretKey"];
            var Key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //------------------------
            var Token = new JwtSecurityToken
                (
                issuer: configuraion["JWToptions:issuer"],
                audience: configuraion["JWToptions:audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
            
        }


    }
}
