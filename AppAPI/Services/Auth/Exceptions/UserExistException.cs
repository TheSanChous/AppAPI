using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Services.Auth.Exceptions
{
    public class UserExistException : Exception
    {
        public UserExistException() : base()
        {

        }
    }
}
