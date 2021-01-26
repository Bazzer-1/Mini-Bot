using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Bot.service.exception
{
    class WrongAnswerException : Exception
    {
        public WrongAnswerException(string message) : base(message)
        {}
    }
}
