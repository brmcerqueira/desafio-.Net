using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Domain;

namespace Desafio.Persistence
{
    public interface IDefaultDao
    {
        void CreateUser(User user);
        bool HasEmail(string email);
    }
}
