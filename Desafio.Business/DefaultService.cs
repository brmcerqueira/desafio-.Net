using Desafio.Business.Dtos;
using Desafio.Business.Validators;
using Desafio.Domain;
using Desafio.Domain.Exceptions;
using Desafio.Persistence;
using FluentValidation;
using System;
using System.Linq;

namespace Desafio.Business
{
    internal class DefaultService : IDefaultService
    {
        private readonly IDefaultDao dao;
        private readonly SignUpDtoValidator signUpDtoValidator;

        public DefaultService(IDefaultDao dao)
        {
            this.dao = dao;
            signUpDtoValidator = new SignUpDtoValidator();
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
            signUpDtoValidator.Check(dto);

            if (dao.HasEmail(dto.Email))
            {
                throw new EmailAlreadyExistsException();
            }

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
        }
    }
}
