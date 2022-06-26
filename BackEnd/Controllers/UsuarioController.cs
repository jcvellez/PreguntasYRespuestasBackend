﻿using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
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
        public async Task<IActionResult> Post(Usuario usuario)
        {
            try
            {
                await _usuarioServices.SaveUser(usuario);
                return Ok(new{message= "usuario registrado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);               
            }
        }
    }
}
