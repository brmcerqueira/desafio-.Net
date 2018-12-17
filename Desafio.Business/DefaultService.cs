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
        private readonly SignInDtoValidator signInDtoValidator;

        public DefaultService(SigningCredentials signingCredentials, IDefaultDao dao)
        {
            this.signingCredentials = signingCredentials;
            this.dao = dao;
            signUpDtoValidator = new SignUpDtoValidator();
            signInDtoValidator = new SignInDtoValidator();
        }

        public object Me(int userId)
        {
            var user = dao.GetUserById(userId);

            return new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.CreatedAt,
                user.LastLogin,
                Phones = user.Phones.Select(p => new
                {
                    p.AreaCode,
                    p.CountryCode,
                    p.Number
                })
            };
        }

        public string SignIn(ISignInDto dto)
        {
            signInDtoValidator.Check(dto);

            var user = dao.GetUserByEmail(dto.Email);

            if (user == null || !BCryptHelper.CheckPassword(dto.Password, user.Password))
            {
                throw new AuthenticationException();
            }

            var now = DateTime.Now;

            user.LastLogin = now;

            dao.UpdateUser(user);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor
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
                Expires = now + TimeSpan.FromDays(3)
            }));
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
