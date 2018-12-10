using AutoMapper;
using LunchManagement.Models;
using LunchManagement.Repository;
using LunchManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly IPasswordHasher<AppUser> passwordHasher;
        private readonly ILogger<CustomerService> logger;
        private readonly IConfiguration configurationRoot;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher, ILogger<CustomerService> logger, IConfiguration configurationRoot) : base(customerRepository)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.logger = logger;
            this.configurationRoot = configurationRoot;
        }

        public async Task<IActionResult> AddAppUserAsync(RegistrationViewModel model)
        {
            var userIdentity = mapper.Map<AppUser>(model);
            var insertedUser = await userManager.CreateAsync(userIdentity, model.Password);

            if (userIdentity == null) return new BadRequestObjectResult("Created User failed");

            var newCustomer = new Customer
            {
                IdentityId = userIdentity.Id
            };

            await this.customerRepository.Add(newCustomer);

            return new OkObjectResult("Account created");
        }

        public async Task<IActionResult> CreateToken(LoginViewModel model)
        {
            try
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return new UnauthorizedResult();
                }
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var userClaims = await userManager.GetClaimsAsync(user);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                    }.Union(userClaims);

                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationRoot["JwtSecurityToken:Key"]));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: configurationRoot["JwtSecurityToken:Issuer"],
                        audience: configurationRoot["JwtSecurityToken:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signingCredentials
                        );
                    var response =  new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        expiration = jwtSecurityToken.ValidTo
                    };

                    return new OkObjectResult(response);
                }
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                logger.LogError($"error while creating token: {ex}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
