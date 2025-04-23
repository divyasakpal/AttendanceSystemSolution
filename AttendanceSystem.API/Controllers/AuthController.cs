using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AttendanceSystem.Repository.Interfaces;
using AttendanceSystem.Shared;
using AttendanceSystem.Shared.DTOs;
using AttendanceSystem.Shared.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AttendanceSystem.API.Controllers
{

    /// <summary>
    ///  JWT authorization and authontication.
    /// </summary>
    public class AuthController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private IValidator<RegisterDto> _registerValidator;
        private IValidator<LoginRequestDto> _loginValidator;
        ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger ,
                                UserManager<IdentityUser> userManager,
                                ITokenRepository tokenRepository,
                                IValidator<LoginRequestDto> loginValidator,
                                IValidator<RegisterDto> registerValidator)
        {    _logger = logger;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }


        /// <summary>
        /// To register a service user with roles- reader/writter.
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                ValidationResult result = await _registerValidator.ValidateAsync(registerDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                var identityUser = new IdentityUser()
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.UserName,
                };

                var identityResult = await _userManager.CreateAsync(identityUser, registerDto.Password);
                if (identityResult.Succeeded)
                {
                    if (registerDto.Roles != null && registerDto.Roles.Any())
                        identityResult = await _userManager.AddToRolesAsync(identityUser, registerDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok(Constants.UserRegistered);
                    }
                }
                return BadRequest(Constants.ErrorSomethingWentWrong);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }

        }

        /// <summary>
        /// Service login for JWT token.
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                ValidationResult result = await _loginValidator.ValidateAsync(loginRequestDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);
                if (user != null)
                {
                    var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                    if (checkPasswordResult)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Any())
                        {
                            var jwttoken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                            var LoginResponseDto = new LoginResponseDto() { JwtToken = jwttoken };
                            return Ok(LoginResponseDto);
                        }
                        else return Unauthorized(Constants.ErrorUnauthorizedUser);
                    }
                    else return BadRequest(Constants.ErrorIncorrectPassword);
                }
                else
                {
                    return BadRequest(Constants.ErrorIncorrectUserName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }
    }
}
