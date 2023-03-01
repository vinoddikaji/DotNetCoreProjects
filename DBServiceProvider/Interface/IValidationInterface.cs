using DBServiceProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServiceProvider.Interface
{
    public interface IValidationInterface
    {
        public int SaveUserDetails(UserRegistration userRegistration);
        public bool CheckIsUserAvailable(string EmailId);
        public bool CheckLoginCredentials(UserRegistration userRegistration);

        public UserRegistration GetUserDetails(string EmailId);
    }
}
