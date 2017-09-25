using System;
using System.Collections.Generic;
using System.Text;

namespace Ekoodi.Utilities.Bank
{
    //---------
    // New exception for virtual bar code creation
    //---------
    public class InvalidVirtualBarCodeException : Exception
    {
        public InvalidVirtualBarCodeException()
        {
        }

        public InvalidVirtualBarCodeException(string message)
            : base(message)
        {
        }

        public InvalidVirtualBarCodeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    //---------------------------------------------------

    public class VirtualBarCode
    {
        public string virtualBarCodeStr { get; }

        public VirtualBarCode()
        {
        }

        public VirtualBarCode(string iBAN, string reference, string value, string dueDate)
        {
            virtualBarCodeStr = "";

            // Use symbol version 5
            if (reference.Substring(0, 2) == "RF")
            {
                virtualBarCodeStr = "4347367347";
            }
            else
            // Use symbol version 4
            {
                virtualBarCodeStr += "4";
                virtualBarCodeStr += iBAN.Substring(2);
            }
        }
    }
}
