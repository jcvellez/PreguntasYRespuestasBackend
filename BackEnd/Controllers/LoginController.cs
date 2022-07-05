using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Utilss;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginservice;
        public LoginController(ILoginService loginservice)
        {
            _loginservice = loginservice;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                var user = await _loginservice.ValidateUser(usuario);
                if (user == null)
                {
                    return BadRequest(new { message= "Usuario o contraseña invalidos" });
                }
                return Ok(new { usuario =user.NombreUsuario });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
