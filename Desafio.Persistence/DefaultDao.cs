using Desafio.Domain;
using System.Linq;

namespace Desafio.Persistence
{
    internal class DefaultDao : IDefaultDao
    {
        private readonly DaoContext context;

        public DefaultDao(DaoContext context)
        {
            this.context = context;
        }

        public void CreateUser(User user)
        {
            context.Add(user);
            context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return context.Set<User>().SingleOrDefault(e => e.Email == email);
        }

        public bool HasEmail(string email)
        {
            return context.Set<User>().Any(e => e.Email == email);
        }
    }
}
