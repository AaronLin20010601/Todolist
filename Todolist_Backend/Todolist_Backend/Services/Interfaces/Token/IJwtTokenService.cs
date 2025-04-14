using Todolist_Backend.Models.Entities;

namespace Todolist_Backend.Services.Interfaces.Token
{
    public interface IJwtTokenService
    {
        string CreateJwtToken(User user);
    }
}
