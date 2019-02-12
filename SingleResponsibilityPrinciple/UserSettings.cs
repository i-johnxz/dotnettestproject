using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    class UserSettings
    {
        private User _user;
        private UserAuth _auth;

        public UserSettings(User user)
        {
            _user = user;
        }

        public void ChangeSettings(Settings settings)
        {
            if (_auth.VerifyCredentials())
            {

            }
        }
    }
}
