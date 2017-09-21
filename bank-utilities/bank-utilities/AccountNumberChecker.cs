using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Ekoodi.Utilities.Bank
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
                // Check length of second part after hyphen
                string partOfString;
                partOfString = accNumber.Substring(hyphenPos + 1);

                // Length must be 2-8
                if (partOfString.Length < 2 || partOfString.Length > 8)
                {
                    throw new InvalidAccountNumberException("Length of second part is invalid");
                }
            }

            // Remove possible hyphen and check number validity
            string hyphenRemoved = accNumber.Replace("-", "");

            // Check length
            if (hyphenRemoved.Length<8 || hyphenRemoved.Length > 14)
            {
                throw new InvalidAccountNumberException("Number length is invalid");
            }

            // Check number validity
            long converted;
            bool validNumber = long.TryParse(hyphenRemoved, out converted);

            if (!validNumber)
            {
                throw new InvalidAccountNumberException("Not a valid number");
            }
//        }
        }

        //---------------
        // Generate long account number
        //---------------
        private string GenerateLongFormat(string accNumber)
        {
            string[] groupA = new string[] { "1", "2", "3", "6", "7", "8" };
            string[] groupB = new string[] { "4", "5" };

            // Remove hyphen, if it exists
            if (accNumber.IndexOf("-")!=-1)
            {
                accNumber = accNumber.Remove(6, 1);
            }

            // Check which method to use when filling zeros
            if (Array.IndexOf(groupA, accNumber.Substring(0,1))!=-1)
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
        // Input: Bank account number, length 14, no hyphen
        // Throws InvalidAccountNumberException if check digit is not correct
        //---------------
        private void LastDigitCheck(string accNumber)
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
        // Generate IBAN account number
        //---------------
        private string GenerateIbanFormat(string accNumber)
        {
            int checkNumber =98;
            BigInteger mod;

            // Generate check number
            // Loop and try to get mod 1 from the calculation
            do
            {
                checkNumber--;

                // Create the number first in string ("1518" = "FI")
                string tryThisNumber = accNumber + "1518" + checkNumber.ToString("00");
                BigInteger theBigNumber = BigInteger.Parse(tryThisNumber);
                mod = theBigNumber % 97;

            } while (mod != 1 && checkNumber >=0);

            return "FI"+ checkNumber.ToString("00 ") + accNumber.Substring(0,4) + " " + accNumber.Substring(4,4)
                + " " + accNumber.Substring(8,4)+ " " + accNumber.Substring(12);
        }

        //---------------
        // GetLongFormat
        //---------------
        public string GetLongFormat (string accNumber)
        {
            CheckFormat(accNumber);
            string newNumber = GenerateLongFormat(accNumber);
            LastDigitCheck (newNumber);
            return newNumber;
        }

        //---------------
        // Get Iban format
        //---------------
        public string GetIbanFormat(string accNumber)
        {
            string newNumber = GetLongFormat(accNumber);
            string ibanStr = GenerateIbanFormat(newNumber);

            return ibanStr;
        }

    }
}
