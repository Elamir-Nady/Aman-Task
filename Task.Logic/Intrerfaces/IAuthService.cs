using Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrerfaces
{
    public interface IAuthService
    {
        Task<AuthModel> SignUp(RegisterModel signUpModel);
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> Login(TokenRequestModel model);

    }
}
