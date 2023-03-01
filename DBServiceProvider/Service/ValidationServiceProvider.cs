using DBServiceProvider.Database.Models;
using DBServiceProvider.Interface;
using DBServiceProvider.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServiceProvider.Service
{
    public class ValidationServiceProvider : IValidationInterface
    {

        private readonly ILogger<ValidationServiceProvider> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;


        public ValidationServiceProvider(ILogger<ValidationServiceProvider> logger, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = applicationDbContext;
        }


        public int SaveUserDetails(UserRegistration userRegistration)
        {
            try
            {
                _dbContext.UserRegistration.Add(userRegistration);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return 0;
        }

        public bool CheckIsUserAvailable(string EmailId)
        {
            var IsUserAvailable = false;
            try
            {
                IsUserAvailable=_dbContext.UserRegistration.Any(e => e.Email == EmailId);
            }
            catch (Exception ex)
            {

                throw;
            }

            return IsUserAvailable;
        }


        public bool CheckLoginCredentials(UserRegistration userRegistration)
        {
            var IsValidCredentials = false;
            try
            {
                var isUserAvailable = _dbContext.UserRegistration.Any(e => e.Email == userRegistration.Email);
                if (isUserAvailable) {
                    var user = _dbContext.UserRegistration.Count(e => e.Email == userRegistration.Email & e.Password == userRegistration.Password);
                    if (user > 0)
                    {
                        IsValidCredentials = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return IsValidCredentials;
        }

        public UserRegistration GetUserDetails(string EmailId)
        {
            return _dbContext.UserRegistration.Where(u => u.Email == EmailId).AsQueryable().FirstOrDefault();
        }
    }
}
