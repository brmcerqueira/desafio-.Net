﻿using Desafio.Business.Dtos;

namespace Desafio.Business
{
    public interface IDefaultService
    {
        void SignUp(ISignUpDto dto);
        object SignIn(ISignInDto dto);
        object Me();
    }
}