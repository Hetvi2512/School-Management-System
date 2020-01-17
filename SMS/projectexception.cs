using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS
{
    class projectException
    {
    }

    class PassLengthNotValidException : ApplicationException
    {
        public PassLengthNotValidException(string s)
            : base(s)
        {

        }
    }
    class class4
    {
        public void checkpass(int a)
        {
            if (a < 8)
            {
                throw new PassLengthNotValidException("Password should be more than 8 character");
            }

        }
    }
}