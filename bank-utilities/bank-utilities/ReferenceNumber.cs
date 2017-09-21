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
    // ReferenceNum class
    //---------
    public class ReferenceNum
    {
        // Properties
        public string RefNumber { get; }

        // Private fields
        private string _basePart;

        // Constructors
        public ReferenceNum()
        {

        }

        public ReferenceNum(string refNum)
        {
            _basePart = refNum.Substring(0, refNum.Length - 1);
            CheckRefNum(refNum);
            RefNumber = refNum;
        }

        // Methods
        public string NextRefNumber()
        {
            return "FF";
        }

        public void SetBasePart(string basePart)
        {
            _basePart = basePart;
        }

        // Reference number check
        private void CheckRefNum(string refNum)
        {
            int sum=0;
            int[] factors = new int[] { 7, 3, 1 };
            int factorInd = 0;

            // Calculate the check digit
            for (int i=_basePart.Length-1; i>=0; i--)
            {
                int x = int.Parse(_basePart.Substring(i, 1));
                sum = sum + x * factors[factorInd];
                factorInd++;

                if (factorInd > 2)
                    factorInd = 0;
            }

            // Calculate next ten
            int nextTen = sum + (10 - sum % 10);
            int checkDigit = nextTen - sum;
            if (checkDigit == 10)
                checkDigit = 0;

            // Throw exception in the numbers does not match
            if (checkDigit.ToString() != refNum.Substring(refNum.Length - 1, 1)){
                throw new InvalidRefNumberException("Invalid check digit");
            }
        }
    }
}
