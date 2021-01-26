using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Bot.service.exception
{
    class WrongSushiException : Exception
    {
        public WrongSushiException(string message) : base(message)
        {
        }
    }
}
