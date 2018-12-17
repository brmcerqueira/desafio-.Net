using Desafio.Domain;

namespace Desafio.Persistence
{
    public interface IDefaultDao
    {
        void CreateUser(User user);
        bool HasEmail(string email);
        User GetUserByEmail(string email);
        void UpdateUser(User user);
        User GetUserById(int userId);
    }
}
