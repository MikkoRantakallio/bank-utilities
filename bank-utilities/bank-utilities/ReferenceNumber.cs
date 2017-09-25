using System;
using System.Text;

namespace Ekoodi.Utilities.Bank
{
    //---------
    // New exception class for referencenumber check
    //---------
    public class InvalidRefNumberException : Exception
    {
        public InvalidRefNumberException()
        {
        }

        public InvalidRefNumberException(string message)
            : base(message)
        {
        }

        public InvalidRefNumberException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    //---------
    // ReferenceNumber class
    //---------
    public class ReferenceNumber
    {
        //-------
        // Properties
        //-------
        public string RefNumberFI { get; set; }
        public string BasePartFI { get; set; }

        //-------
        // Constructors
        //-------

        // Default
        public ReferenceNumber()
        {
            BasePartFI = "";
            RefNumberFI = "";
        }

        // Constructor with refNumber
        public ReferenceNumber(string refNum)
        {
            CheckRefNum(refNum);
            BasePartFI = refNum.Substring(0, refNum.Length - 1);
            RefNumberFI = FormatRefNumber(refNum);
        }

        //---------
        // Methods
        //---------

        // Format reference number
        private string FormatRefNumber(string refNum)
        {
            string finalString="";
            if (refNum.Length > 5)
            {
                int startPos = refNum.Length;
                do
                {
                    startPos -= 5;
                    finalString = " " + refNum.Substring(startPos, 5) + finalString;

                } while (startPos > 5);

                finalString = refNum.Substring(0, startPos) + finalString;
                return finalString;
            }
            else
            {
                return refNum;
            }
        }

        // Generate reference number based on base part
        public string GenerateRefNumber(string basePart)
        {
            if (basePart == "" || basePart.Length<4 || basePart.Length >19)
            {
                throw new InvalidRefNumberException("Base part is missing or invalid length");
            }
            else
            {
                string checkDigit = CalculateCheckDigit(basePart);
                return FormatRefNumber(basePart + checkDigit);
            }
        }

        // Calculate Check digit
        private string CalculateCheckDigit(string basePart)
        {
            int[] factors = new int[] { 7, 3, 1 };
            int factorIndex = 0;
            int sum = 0;

            for (int i = basePart.Length - 1; i >= 0; i--)
            {
                int x = int.Parse(basePart.Substring(i, 1));
                sum = sum + x * factors[factorIndex];
                factorIndex++;

                if (factorIndex > 2)
                    factorIndex = 0;
            }

            // Calculate next ten
            int nextTen = sum + (10 - sum % 10);
            int checkDigit = nextTen - sum;
            if (checkDigit == 10)
                checkDigit = 0;

            return checkDigit.ToString();
        }

        // Reference number check
        public void CheckRefNum(string refNum)
        {
            string basePartFI = refNum.Substring(0, refNum.Length - 1);
            string checkDigit = CalculateCheckDigit(basePartFI);

            // Throw exception if the numbers does not match
            if (checkDigit != refNum.Substring(refNum.Length - 1, 1)){
                throw new InvalidRefNumberException("Invalid finnish check digit");
            }
        }
    }
}
