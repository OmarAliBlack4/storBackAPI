using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.BusinessLogicLayer.DTOs.AuthDTOs;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;

namespace ProjectAPI.PresentationLayer.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthonticationController(IAuthonticationService authonticationService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO loginDTO)
        {
            var result = await authonticationService.Login(loginDTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await authonticationService.Register(registerDTO);
            return Ok(result);
        }

    }
}
