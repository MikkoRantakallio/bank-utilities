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

            // Symbol version 5
            if (reference.Substring(0, 2) == "RF")
            {
                virtualBarCodeStr = "5";
                virtualBarCodeStr += iBAN.Substring(2);
                virtualBarCodeStr += GetEurosAndCentsStr(value);
                virtualBarCodeStr += GetRefStr(reference, 5);
                virtualBarCodeStr += GetDueDateStr(dueDate);

                int checkNumber = Calculate103Modulo(virtualBarCodeStr);

                // Add start sign, check digit and stop sign
                virtualBarCodeStr = "[105]" + virtualBarCodeStr + "[" + checkNumber.ToString() + "][stop]";
            }
            else
            // Symbol version 4
            {
                virtualBarCodeStr += "4";
                virtualBarCodeStr += iBAN.Substring(2);
                virtualBarCodeStr += GetEurosAndCentsStr(value);
                virtualBarCodeStr += "000";
                virtualBarCodeStr += GetRefStr(reference, 4);
                virtualBarCodeStr += GetDueDateStr(dueDate);

                int checkNumber = Calculate103Modulo(virtualBarCodeStr);

                // Add start sign, check digit and stop sign
                virtualBarCodeStr = "[105]" + virtualBarCodeStr + "[" + checkNumber.ToString() + "][stop]";
            }
        }

        private string GetEurosAndCentsStr(string eValue)
        {
            if (eValue == "00000000")
                return eValue;

            int commaPos = eValue.IndexOf(",");

            string euros = eValue.Substring(0, commaPos);
            string cents = eValue.Substring(commaPos + 1);

            // Create final euro string
            int zerosNeeded = 6 - euros.Length;
            string zeros = new string('0', zerosNeeded);
            euros = zeros + euros;

            // Add trailing zero to cents if needed
            if (cents.Length == 1)
                cents += "0";

            return euros + cents;
        }

        private string GetRefStr(string refStr, int mode)
        {
            if (mode == 4)
            {
                // Create final reference number string, length 20, leading zeros
                int zerosNeeded = 20 - refStr.Length;
                string zeros = new string('0', zerosNeeded);

                return zeros + refStr;
            }
            if (mode == 5)
            {
                // After "RF" is removed, insert needed amount of zeros after the second character
                string ref5 = refStr.Substring(2);
                int strLen = ref5.Length;
                int zerosNeeded = 23 - strLen;
                string zeros = new string('0', zerosNeeded);
                string newRef = ref5.Insert(2, zeros);

                return newRef;
            }
            return "";
        }

        private string GetDueDateStr ( string dueDate)
        {
            string year = dueDate.Substring(dueDate.Length - 2);
            string month = dueDate.Substring(3, 2);
            string day = dueDate.Substring(0, 2);
            return year+month+day;
        }

        private int Calculate103Modulo(string barCode)
        {
            // Start char
            int sum = 105;

            for (int i=0; i < 27; i++)
            {
                int startInd = i * 2;
                string node = barCode.Substring(startInd, 2);

                sum += int.Parse(node) *(i+1);
            }

            int mod103 = sum % 103;
            return mod103;
        }
    }
}
