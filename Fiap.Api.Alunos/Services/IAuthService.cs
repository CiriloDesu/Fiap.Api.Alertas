using Fiap.Web.Alunos.Models;

namespace Fiap.Api.Alunos.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string username, string password);

    }
}
