using LightInject;

namespace Desafio.Persistence
{
    public class PersistenceRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDefaultDao, DefaultDao>();
        }
    }
}
