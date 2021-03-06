using Intrerfaces;
using Microsoft.AspNetCore.Http;
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

        public IUnitOfWork _UnitOfWork { get; }

        public AuthController(IAuthService authService,IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _UnitOfWork = unitOfWork;
        }


        public ActionResult register()
        {
            if (HttpContext.Session.GetString("_Token") != null)
            {
                return RedirectToAction("index", "Product");
            }
            return View();
        }

        [HttpPost("register")]
        public async Task<ActionResult> register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var result = await _UnitOfWork.GetAuthRepo().SignUp(model);

            if (!result.IsAuthenticated)
                return NotFound(result.Message);
            HttpContext.Session.SetString("_Token", result.Token);

            return RedirectToAction("index", "Product");
        }
        public ActionResult Login()
        {
            if (HttpContext.Session.GetString("_Token") != null)
            {
                return RedirectToAction("index", "Product");
            }
            return View();
        }
        [HttpPost("Login")]
        public virtual async Task<ActionResult> Login(TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var result = await _UnitOfWork.GetAuthRepo().Login(model);

            if (!result.IsAuthenticated)
                return NotFound(result.Message);
            HttpContext.Session.SetString("_Token", result.Token);

            return RedirectToAction("index", "Product");
        }

    }
}
