﻿using AcmeMedicalClinicWebApi.BLL.Interfaces;
using AcmeMedicalClinicWebApi.BLL.Models;
using AcmeMedicalClinicWebApi.Common.Services;
using AcmeMedicalClinicWebApi.DAL.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace AcmeMedicalClinicWebApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountLogic _accountLogic;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserService userService, IAccountLogic auth)
        {
            _userService = userService;
            _accountLogic = auth;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeModel model)
        {
            var userExists = await _userService.FindUserByName(model.UserName);

            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            else
            {
                var result = await _accountLogic.RegisterEmployee(model);

                if (result.Succeeded)
                    return Ok(new { Status = "Success", Message = "User created successfully!" });

                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                }
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientModel model)
        {
            var userExists = await _userService.FindUserByName(model.UserName);

            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            else
            {
                var result = await _accountLogic.RegisterPatient(model);

                if (result.Succeeded)
                    return Ok(new { Status = "Success", Message = "User created successfully!" });

                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                }
            }
        }

    }
}
