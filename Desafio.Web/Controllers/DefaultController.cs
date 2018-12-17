using Desafio.Business;
using Desafio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Desafio.Web.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IDefaultService service;

        public DefaultController(IDefaultService service)
        {
            this.service = service;
        }

        [HttpPost("signup")]
        public void SignUp(SignUpModel model)
        {
            service.SignUp(model);
        }

        [HttpPost("signin")]
        public string SignIn(SignInModel model)
        {
            return service.SignIn(model);
        }

        [Authorize("Bearer")]
        [HttpGet("me")]
        public object Me()
        {
            return service.Me(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
    }
}