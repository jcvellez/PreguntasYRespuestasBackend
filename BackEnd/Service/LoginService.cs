using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginrepository;
        public LoginService(ILoginRepository loginrepository)
        {
            _loginrepository = loginrepository;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _loginrepository.ValidateUser(usuario);
        }
    }
}
