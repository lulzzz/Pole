﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pole.EventBus.Exceptions
{
    public class PoleEventHandlerImplementException : Exception
    {
        public PoleEventHandlerImplementException(string message) : base(message)
        {

        }
    }
}
