using LightInject;
using System;

namespace Desafio.Business
{
    public class BusinessRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDefaultService, DefaultService>();
        }
    }
}
