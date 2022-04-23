using Intrerfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        public ActionResult register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<ActionResult> register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return NotFound(result.Message);

                return Redirect("/product/index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost("Login")]
        public virtual async Task<ActionResult> Login(TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return NotFound(result.Message);

            return Redirect("/product/index");
        }

    }
}
