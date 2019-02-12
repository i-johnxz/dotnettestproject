using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    class UserAuth
    {
        private User _user;

        public UserAuth(User user)
        {
            _user = user;
        }

        public bool VerifyCredentials()
        {
            return true;
        }
    }
}
