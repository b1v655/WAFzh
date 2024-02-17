﻿using System;

namespace Order.WPF.Model
{
    public class NetworkException : Exception
    {
        public NetworkException(String message) : base(message)
        {
        }

        public NetworkException(Exception innerException) : base("Exception occurred.", innerException)
        {
        }
    }
}