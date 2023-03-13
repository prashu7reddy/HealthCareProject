﻿using HealthCareProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public AccountsController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDetails userDetails)
        {
            if (userDetails == null)
            {
                return BadRequest();
            }

            this.CreatePasswordHash(userDetails.Password, out byte[] passwordSalt, out byte[] passwordHash);
            userDetails.PasswordHash = passwordHash;
            userDetails.PasswordSalt = passwordSalt;
            _dbContext.Users.Add(userDetails);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("PatientLogin")]
        public IActionResult PatientLogin([FromBody] UserLogin login)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.UserName == login.UserName && x.Role=="Patient");

            if (currentUser == null)
            {
                return BadRequest("Invalid Username");
            }

            var isValidPassword = VerifyPassword(login.Password, currentUser.PasswordSalt, currentUser.PasswordHash);

            if (!isValidPassword)
            {
                return BadRequest("Invalid Password");
            }

            var token = GenerateToken(currentUser);

            if (token == null)
            {
                return NotFound("Invalid credentials");
            }

            return Ok(token);
        }

        [NonAction]
        public string GenerateToken(UserDetails userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userDetails.UserName),
                new Claim(ClaimTypes.Email, userDetails.Email),
                new Claim(ClaimTypes.GivenName, userDetails.FullName),
                new Claim(ClaimTypes.Role, userDetails.Role),

            };

            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],

                                             claims: myClaims,
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [NonAction]
        public void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        [NonAction]
        public bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return hash.SequenceEqual(passwordHash);
            }
        }


        [HttpGet("GetName"), Authorize]
        public IActionResult GetName()
        {
            var UserName = User.FindFirstValue(ClaimTypes.Name);
            return Ok(UserName);
        }
        [HttpGet("GetRole")]
        public IActionResult GetRole()
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(Role);
        }


        ////////////////////////////
        
        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin([FromBody] UserLogin login)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.UserName == login.UserName  && x.Role=="Admin");

            if (currentUser == null)
            {
                return BadRequest("Invalid Username");
            }

            var isValidPassword = VerifyPassword(login.Password, currentUser.PasswordSalt, currentUser.PasswordHash);

            if (!isValidPassword)
            {
                return BadRequest("Invalid Password");
            }

            var token = GenerateToken(currentUser);

            if (token == null)
            {
                return NotFound("Invalid credentials");
            }

            return Ok(token);
        }
        /////////////////////
        [HttpPost("DoctorLogin")]
        public IActionResult DoctorLogin([FromBody] UserLogin login)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.UserName == login.UserName && x.Role == "Doctor");

            if (currentUser == null)
            {
                return BadRequest("Invalid Username");
            }

            var isValidPassword = VerifyPassword(login.Password, currentUser.PasswordSalt, currentUser.PasswordHash);

            if (!isValidPassword)
            {
                return BadRequest("Invalid Password");
            }

            var token = GenerateToken(currentUser);

            if (token == null)
            {
                return NotFound("Invalid credentials");
            }

            return Ok(token);
        }
    }
}

