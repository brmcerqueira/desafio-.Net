using FluentValidation;
using LightInject;

namespace Desafio.Business
{
    public class BusinessRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ValidationLanguageManager>();
            serviceRegistry.Register<IDefaultService, DefaultService>();
        }
    }
}
