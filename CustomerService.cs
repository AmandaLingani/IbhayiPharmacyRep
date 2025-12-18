using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PrescribingSystem.Data;
using PrescribingSystem.Models;
using PrescribingSystem.Models.ViewModels;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;


namespace PrescribingSystem.Services
{
    public class CustomerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ILogger<CustomerService> logger)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;

        }

        public async Task<(ApplicationUser? Customer, IEnumerable<string> Errors)> RegisterCustomerAsync(CustomerAdditionViewModel model, IUrlHelper urlHelper, string requestScheme)
        {
            string autoPassword = GeneratePassword();

            var customer = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CellphoneNumber = model.PhoneNumber,
                IdentityNumber = model.IdentityNumber,
                UserName = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(customer, autoPassword);
            _logger.LogInformation("[DEBUG] CreateAsync result: {Result}", result.Succeeded);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogWarning("[DEBUG] Identity Error: {Error}", error.Description);
                }
                return (null, result.Errors.Select(e => e.Description).ToList());
            }

            _logger.LogInformation("[DEBUG] Customer created with ID {Id}", customer);
 

             await _userManager.AddClaimAsync(customer, new Claim("FirstName", customer.FirstName));
            await _userManager.AddClaimAsync(customer, new Claim("LastName", customer.LastName));
           
            await _userManager.AddToRoleAsync(customer, "Customer");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(customer);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            string confirmationLink = urlHelper.Page(
          "/Account/ConfirmEmail",
          pageHandler: null,
          values: new { area = "Identity", userId = customer.Id, code = code },
          protocol: requestScheme
             );

            await _emailSender.SendEmailAsync(customer.Email, "Ibhayi Pharmacy Account",
               $"An account with Ibhayi Pharmacy has been created for you. <br>" +
               $"Dear Customer, <br>"+
               $"Please confirm your email by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.<br><br>" +
               $"Your login details: <br>" +
               $"Password: <b>{autoPassword}</b><br>" +
               $"You cannot log in until your email is confirmed, please use above details to login once account has been confirmed.");

            return (customer, Enumerable.Empty<string>());
        }
        private string GeneratePassword()
        {
            return "Ph@" + Guid.NewGuid().ToString("N").Substring(0, 8) + "1!";
        }

        
    }
}
