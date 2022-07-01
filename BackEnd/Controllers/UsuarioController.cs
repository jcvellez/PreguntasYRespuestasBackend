using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Utilss;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServices;
        public UsuarioController(IUsuarioService usuarioServices)
        {
            this._usuarioServices = usuarioServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {
            try
            {
                var validateExistence = await _usuarioServices.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "usuario " + usuario.NombreUsuario + " ya existe" });
                }
                //usuario.Password = Encriptar(usuario.Password);
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                await _usuarioServices.SaveUser(usuario);
                return Ok(new { message = "usuario registrado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var obj= _usuarioServices.SaveUser(usuario);
            //return Ok(new { message = "usuario registrado con exito" });            
        }
    }
}
