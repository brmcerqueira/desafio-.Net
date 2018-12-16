using Desafio.Business.Dtos;
using Desafio.Domain;
using Desafio.Domain.Exceptions;
using Desafio.Persistence;
using System;
using System.Linq;

namespace Desafio.Business
{
    internal class DefaultService : IDefaultService
    {
        private readonly IDefaultDao dao;

        public DefaultService(IDefaultDao dao)
        {
            this.dao = dao;
        }

        public object Me()
        {
            return new
            {
                FirstName = ""
            };
        }

        public object SignIn(ISignInDto dto)
        {
            return new
            {
                Token = ""
            };
        }

        public void SignUp(ISignUpDto dto)
        {
            dao.CreateUser(new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                CreatedAt = DateTime.Now,
                Phones = dto.Phones.Select(p => new UserPhone
                {
                    AreaCode = p.AreaCode,
                    CountryCode = p.CountryCode,
                    Number = p.Number,
                }).ToList()
            });
            //throw new EmailAlreadyExistsException();
        }
    }
}
