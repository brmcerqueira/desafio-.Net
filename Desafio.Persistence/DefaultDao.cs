using Desafio.Domain;

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
    }
}
