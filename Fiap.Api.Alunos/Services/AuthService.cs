using Fiap.Api.Alunos.Services;
using Fiap.Web.Alunos.Models;

namespace Fiap.Web.Alunos.Services
{
    public class AuthService : IAuthService
    {
        private List<UserModel> _users = new List<UserModel>
                {
                    new UserModel { UserId = 1, Username = "usuario", Password = "pass123", Role = "usuario" },
                    new UserModel { UserId = 2, Username = "admin", Password = "pass123", Role = "admin" },
                };


        public UserModel Authenticate(string username, string password)
        {
            // Aqui você normalmente faria a verificação de senha de forma segura
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
