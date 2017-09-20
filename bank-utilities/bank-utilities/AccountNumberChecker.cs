using System;
using System.Collections.Generic;
using System.Text;

namespace Ekoodi.Utilities.Checker
{
    //---------
    // New exception for account number check
    //---------
    public class InvalidAccountNumberException : Exception
    {
        public InvalidAccountNumberException()
        {
        }

        public InvalidAccountNumberException(string message)
            : base(message)
        {
        }

        public InvalidAccountNumberException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    //---------
    // AccountNumberChecker class
    //---------
    class AccountNumberChecker
    {
        // Constructor
        public AccountNumberChecker()
        {
        }

        //---------------
        // Check number format
        //---------------
        private void CheckFormat(string accNumber)
        {
            // Hyphen must be in position 7
            int hyphenPos = accNumber.IndexOf("-");

            if (hyphenPos != 6 && hyphenPos != -1)
            {
                throw new InvalidAccountNumberException("Hyphen position is invalid");
            }

            // Check that given string is a number, hyphen included in the string
            if (hyphenPos == 6)
            {
                // Check first part before hyphen
                int converted;
                string partOfString = accNumber.Substring(0, 6);
                bool validNumber = int.TryParse(partOfString, out converted);

                if (!validNumber)
                {
                    throw new InvalidAccountNumberException("First part is not a valid number");
                }

                // Check second part after hyphen
                partOfString = accNumber.Substring(hyphenPos+1);

                // Length must be 2-8
                if (partOfString.Length < 2 || partOfString.Length > 8)
                {
                    throw new InvalidAccountNumberException("Length of second part is invalid");
                }

                // String part must be number
                validNumber = int.TryParse(partOfString, out converted);

                if (!validNumber)
                {
                    throw new InvalidAccountNumberException("Second part is not a valid number");
                }
            }
            else

            // Hyphen not included in the string, length must be ok and also number validity
            {
                // Check length
                if (accNumber.Length<8 || accNumber.Length > 14)
                {
                    throw new InvalidAccountNumberException("Number length is invalid");
                }

                // Check number validity
                long converted;
                bool validNumber = long.TryParse(accNumber, out converted);

                if (!validNumber)
                {
                    throw new InvalidAccountNumberException("Not a valid number");
                }
            }
        }

        //---------------
        // Generate long account number
        //---------------
        private string GenerateLongFormat(string accNumber)
        {
            string[] groupA = new string[] { "1", "2", "31", "33", "34", "36", "37", "38", "39", "6", "8" };
            string[] groupB = new string[] { "4", "5" };

            // Remove hyphen, if it exists
            if (accNumber.IndexOf("-")!=-1)
            {
                accNumber = accNumber.Remove(6, 1);
            }

            // Check which method to use when filling zeros
            if (Array.IndexOf(groupA, accNumber.Substring(0,1))!=-1 || Array.IndexOf(groupA, accNumber.Substring(0, 2)) != -1)
            {
                string firstPart = accNumber.Substring(0, 6);
                string secondPart = accNumber.Substring(6);
                int zerosNeeded = 8 - secondPart.Length;
                string zeros = new string ('0', zerosNeeded);

                return firstPart + zeros + secondPart;
            }
            else if (Array.IndexOf(groupB, accNumber.Substring(0, 1)) != -1)
            {
                string firstPart = accNumber.Substring(0, 7);
                string secondPart = accNumber.Substring(7);
                int zerosNeeded = 7 - secondPart.Length;
                string zeros = new string('0', zerosNeeded);

                return firstPart + zeros + secondPart;
            }
            else
            {
                throw new InvalidAccountNumberException("Not a valid bank group");
            }
        }

        //---------------
        // Check the last digit
        //---------------
        private void CheckDigitCheck(string accNumber)
        {
            int[] factors = new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2};

            string stringToCheck = accNumber.Substring(0, 13);
            int checkSum = 0;

            for (int i=12; i>=0; i--)
            {
                int digit = int.Parse(stringToCheck.Substring(i, 1));
                int weight = digit * factors[i];

                if (weight<10)
                {
                    checkSum += weight;
                }
                else
                {
                    checkSum = checkSum + 1 + weight % 10;
                }
            }
            int nextTen = checkSum + (10 - checkSum % 10);
            int checkDigit = nextTen - checkSum;

            if (checkDigit != int.Parse(accNumber.Substring(13)))
            {
                throw new InvalidAccountNumberException("Invalid check digit");
            }
        }

        //---------------
        // GetLongFormatFI
        //---------------
        public string GetLongFormatFI (string accNumber)
        {
            CheckFormat(accNumber);
            string newNumber = GenerateLongFormat(accNumber);
            CheckDigitCheck (newNumber);
            return newNumber;
        }

        //---------------
        // GetIbanFormat
        //---------------
        public string GetIbanFormat(string accNumber)
        {
            CheckFormat(accNumber);
            string newNumber = GenerateLongFormat(accNumber);
            CheckDigitCheck(newNumber);
            return newNumber;
        }

    }
}
