using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models;
using Intrerfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task.Data.Entites;
using Helpers;
using Microsoft.Extensions.Options;
using Services.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _usermanager;
        private readonly JWT _jwt;

        public IConfiguration Configuration { get; }

        public AuthService(UserManager<User> usermanager, IConfiguration configuration, IOptions<JWT> jwt)
        {
            _usermanager = usermanager;
            Configuration = configuration;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> Login(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _usermanager.FindByEmailAsync(model.Email);
            if (user is null || !(await _usermanager.CheckPasswordAsync(user, model.Password)))
            {
                authModel.Message = "Email or password is inncorrect";
                authModel.IsAuthenticated = false;
                return authModel;
            }

            var userClaims = await _usermanager.GetClaimsAsync(user);
            var roles = await _usermanager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var SigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid",$"{user.Id}")
            }
            .Union(userClaims)
            .Union(roleClaims);



            var Token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new SigningCredentials(SigninKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: Claims
                );
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = Token.ValidTo;
            authModel.Roles = roles.ToList();
            authModel.User_ID = user.Id;
            return authModel;

        }

        public async Task<AuthModel> SignUp(RegisterModel signUpModel)
        {

            if (await _usermanager.FindByEmailAsync(signUpModel.Email) is not null)
                return new AuthModel { Message = "Email is already registered" };


            if (await _usermanager.FindByNameAsync(signUpModel.FirstName) is not null)
                return new AuthModel { Message = "UserName is already registered Please Select another one" };

            User Temp = new User
            {
                UserName = signUpModel.Username,
                Email = signUpModel.Email,
                FirstName = signUpModel.FirstName,
                lasttName = signUpModel.LastName,
            };
            var result = await _usermanager.CreateAsync(Temp, signUpModel.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var err in result.Errors)
                {
                    errors += $"{err.Description} , ";
                }
                return new AuthModel
                {
                    Message = errors
                };
            }

            await _usermanager.AddToRoleAsync(Temp, "User");


            var userClaims = await _usermanager.GetClaimsAsync(Temp);
            var roles = await _usermanager.GetRolesAsync(Temp);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var SigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var Claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name,signUpModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,signUpModel.Email),
                new Claim("uid",$"{Temp.Id}")
            }
           .Union(userClaims)
           .Union(roleClaims);
 



            var Token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new SigningCredentials(SigninKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: Claims
                );


            return new AuthModel
            {
                Email = Temp.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                Username = Temp.UserName,
                User_ID = Temp.Id

            };
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _usermanager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered" };

            if (await _usermanager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered" };

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                lasttName = model.LastName
            };

            var result = await _usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }

                return new AuthModel { Message = errors };
            }

            await _usermanager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user is null || !await _usermanager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;

            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken (User user)
        {
            var userClaims = await _usermanager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",$"{user.Id}")
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
