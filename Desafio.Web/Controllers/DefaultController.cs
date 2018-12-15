using Desafio.Business;
using Desafio.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
        public object SignIn(SignInModel model)
        {
            return service.SignIn(model);
        }

        [HttpGet("me")]
        public object Me()
        {
            return service.Me();
        }
    }
}