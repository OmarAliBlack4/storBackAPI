using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAPI.BusinessLogicLayer.DTOs.AuthDTOs;

namespace ProjectAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthonticationService
    {
        public Task<UserResultDTO> Login(LoginDTO loginDTO);
        public Task<UserResultDTO> Register(RegisterDTO registerDTO);
    }
}
