using System;
using System.Globalization;
namespace JewelryStoreAPI.Helper
{
    public class ErrorHandler: Exception
    {
        public ErrorHandler() : base() { }

        public ErrorHandler(string message) : base(message) { }

        public ErrorHandler(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
