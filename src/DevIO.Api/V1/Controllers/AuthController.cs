﻿using DevIO.Api.Controller;
using DevIO.Api.DTOs.User;
using DevIO.Api.Extensions;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevIO.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;

        public AuthController(INotifier notifier,
                              IOptions<AppSettings> appSettings,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IUser user, ILogger<AuthController> logger) : base(notifier, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Usuário {loginUser.Email} logado com sucesso!");
                return CustomResponse(await GenerateJwt(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                _logger.LogWarning($"Usuário: {loginUser.Email} bloqueado às {DateTime.Now}");
                return CustomResponse(loginUser);
            }

            NotifyError("Usuário ou Senha incorretos");
            _logger.LogWarning($"Usuário: {loginUser.Email} digitou email e/ou senha inválidos;");
            return CustomResponse(loginUser);
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GenerateJwt(user.Email));
            }

            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUser);
        }


        private async Task<LoginResponseDTO> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            AddClaimsToUser(claims, user);

            var identityClaims = CreateIdentityClaims(claims);
            var encodedToken = CreateEncodedToken(_appSettings, identityClaims);

            return new LoginResponseDTO
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiresIn).TotalSeconds,
                UserToken = new UserTokenDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDTO
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };
        }

        private ClaimsIdentity CreateIdentityClaims(IList<Claim> claims)
        {
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private void AddClaimsToUser(IList<Claim> claims, IdentityUser user)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            AddRolesToUser(claims, user);
        }

        private async void AddRolesToUser(IList<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }
        }

        private string CreateEncodedToken(AppSettings appSettings, ClaimsIdentity identityClaims)
        {
            var tokenHandlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var token = tokenHandlder.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = appSettings.Issuer,
                Audience = appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandlder.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
