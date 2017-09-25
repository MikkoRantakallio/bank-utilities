using System;
using System.Numerics;
using Ekoodi.Utilities.Bank;

namespace virtual_bar_code_app
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------
            // Get long value from user
            // -------------------
            string getBigIntValue(string msg, string exitStr, int checkFromIndex, out bool success)
            {
                bool validNumber;
                BigInteger converted;
                string inputStr;
                success = true;

                // Loop until valid number given or input is equal to exit value
                do
                {
                    Console.Write("{0} ", msg);
                    inputStr = Console.ReadLine();

                    if (inputStr.ToUpper() == exitStr)
                    {
                        success = false;
                        return "";
                    }
                    validNumber = BigInteger.TryParse(inputStr.Substring(checkFromIndex), out converted);

                } while (!validNumber);
                return inputStr;
            }

            // -------------------
            // Get decimal value from user
            // -------------------
            string getDoubleValue(string msg, string exitStr, out bool success, double lowLimit, double highLimit)
            {
                bool validNumber;
                double converted;
                string inputStr;
                success = true;

                // Loop until valid number given or input is equal to exit value
                do
                {
                    Console.Write("{0} ", msg);
                    inputStr = Console.ReadLine();

                    if (inputStr.ToUpper() == exitStr)
                    {
                        success = false;
                        return "";
                    }
                    validNumber = double.TryParse(inputStr, out converted);

                    if (converted<lowLimit || converted > highLimit)
                    {
                        Console.WriteLine("Value out of limits {0}-{1}", lowLimit, highLimit);
                        validNumber = false;
                    }

                } while (!validNumber);
                return inputStr;
            }

            // -------------------
            // Get date value from user
            // -------------------
            string getDateValue(string msg, string exitStr, out bool success)
            {
                DateTime dateValue;
                string inputStr;
                success = true;
                int year, month, day;

                // Loop until valid date given or input is equal to exit value
                do
                {
                    Console.Write("{0} ", msg);
                    inputStr = Console.ReadLine();

                    if (inputStr.ToUpper() == exitStr)
                    {
                        success = false;
                        return "";
                    }
                    success = int.TryParse(inputStr.Substring(6,4), out year);
                    success = int.TryParse(inputStr.Substring(3,2), out month);
                    success = int.TryParse(inputStr.Substring(0,2), out day);

                    try
                    {
                        dateValue = new DateTime(year, month, day);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Incorrect date format");
                        success = false;
                    }
                } while (!success);
                return inputStr;
            }

            // --------
            // Show "menu", ask user input, loop until "X" given
            // --------

            bool exitMain = false;
            bool valid;

            do
            {
                // Ask user values for creating virtual bar code
                // IBAN number
                do
                {
                    string bankAccountInput = getBigIntValue("Enter Finnish IBAN bank account number (X=Exit): ", "X", 2, out valid);

                    if (!valid)
                        return;

                    try
                    {
                        BankAccount myAccount = new BankAccount(bankAccountInput);
                    }
                    catch (InvalidAccountNumberException e)
                    {
                        Console.WriteLine(e.Message);
                        valid = false;
                    }
                } while (!valid);

                // Reference number
                string refNumberInput = getBigIntValue("Enter reference number (X=Exit): ", "X", 0, out valid);

                if (!valid)
                    return;

                try
                {
                    ReferenceNumber myAccount = new ReferenceNumber(refNumberInput);
                }
                catch (InvalidRefNumberException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                // Amount
                string amountInput = getDoubleValue("Enter amount (X=Exit): ", "X", out valid, 0, 999999.99);

                if (!valid)
                    return;

                // Due date
                string dueDateInput = getDateValue("Enter due date (PP.KK.VVVV): ", "X", out valid);

                if (!valid)
                    return;

            } while (!exitMain);
        }
    }
}
