using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI.BusinessLogicLayer.DTOs.AuthDTOs;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;
using ProjectAPI.DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.BusinessLogicLayer.Services.Implementations
{
    public class AuthonticationService(UserManager<User> _userManager , IConfiguration _configuration) : IAuthonticationService
    {
        public async Task<UserResultDTO> Login(LoginDTO loginDTO)
        {
            //Check if there is user under this UserName
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user == null) throw new Exception("User not found");

            //Check if password is correct
            var result = await _userManager.CheckPasswordAsync(user,loginDTO.Password);
            if (!result) throw new Exception("Invalid password");

            //create Token and return respons
            var roles = await _userManager.GetRolesAsync(user);
            return new UserResultDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await CtreateTokenAsync(user),
                Role = roles.FirstOrDefault(),
            };
        }

        public async Task<UserResultDTO> Register(RegisterDTO registerDTO)
        {
            var user = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };

            //Create User
            var result = await _userManager.CreateAsync(user , registerDTO.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            if (!string.IsNullOrEmpty(registerDTO.Role))
            {
                await _userManager.AddToRoleAsync(user, registerDTO.Role);
            }


            var roles = await _userManager.GetRolesAsync(user);

            return new UserResultDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await CtreateTokenAsync(user),
                Role = registerDTO.Role,
            };

        }



        private async Task<string> CtreateTokenAsync(User user)
        {
            //Private Cliams
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            //add role roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                audience: _configuration["JwtOptions:Audience"],
                issuer: _configuration["JwtOptions:Issuer"],
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["JwtOptions:DurationInHours"])),
                claims: authClaims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
