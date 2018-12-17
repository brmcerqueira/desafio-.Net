using BCrypt;
using Desafio.Business.Dtos;
using Desafio.Business.Validators;
using Desafio.Domain;
using Desafio.Domain.Exceptions;
using Desafio.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;

namespace Desafio.Business
{
    internal class DefaultService : IDefaultService
    {
        private readonly SigningCredentials signingCredentials;
        private readonly IDefaultDao dao;
        private readonly SignUpDtoValidator signUpDtoValidator;

        public DefaultService(SigningCredentials signingCredentials, IDefaultDao dao)
        {
            this.signingCredentials = signingCredentials;
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
            var user = dao.GetUserByEmail(dto.Email);

            var now = DateTime.Now;

            var handler = new JwtSecurityTokenHandler();

            return new
            {
                Token = handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = "DesafioIssuer",
                    Audience = "DesafioAudience",
                    SigningCredentials = signingCredentials,
                    Subject = new ClaimsIdentity(new GenericIdentity($"{user.FirstName} {user.LastName}", "SignIn"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        }
                    ),
                    NotBefore = now,
                    Expires = now + TimeSpan.FromSeconds(120)
                }))
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
                Password = BCryptHelper.HashPassword(dto.Password, BCryptHelper.GenerateSalt()),
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
