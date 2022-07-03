using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
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

        //localhost:xxxx/api/Usuario/CambiarPassword
        [Route("CambiarPassword")]
        [HttpPut]        
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO request)
        {
            try
            {
                int idUsuario = 6;
                string passEncriptado = Encriptar.EncriptarPassword(request.passwordAnterior);
                var usuario = await _usuarioServices.ValidatePassword(idUsuario,passEncriptado);
                if (usuario == null)
                {
                    return BadRequest(new { message = "La password es incorrecta"});
                }
                else
                {
                    usuario.Password = Encriptar.EncriptarPassword(request.nuevaPassword);
                    await _usuarioServices.UpdatePassword(usuario);
                    return Ok(new { message = "La password fue cambiada con exito con exito" });
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }

    }
}
