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
            string getRefNumber(string msg, string exitStr, out bool success)
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

                    // If RF-style reference number, parse starting from index 2
                    if (inputStr.Substring(0, 2).ToUpper() == "RF")
                    {
                        validNumber = BigInteger.TryParse(inputStr.Substring(2), out converted);
                    }
                    else
                    {
                        validNumber = BigInteger.TryParse(inputStr, out converted);
                    }

                } while (!validNumber);
                return inputStr.ToUpper();
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

                    // Zero allowed
                    if (converted == 0)
                    {
                        return "0,00";
                    }

                    // Too big value, return "00000000"
                    if (converted > highLimit)
                    {
                        return "00000000";
                    }

                    if (inputStr.Substring(inputStr.IndexOf(",") + 1).Length > 2)
                    {
                        Console.WriteLine("Too many decimals");
                        validNumber = false;
                    }

                    // Negatives not allowed
                    if (converted<lowLimit)
                    {
                        Console.WriteLine("Value cannot be negative");
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
                // Empty date is allowed
                do
                {
                    Console.Write("{0} ", msg);
                    inputStr = Console.ReadLine();

                    if (inputStr.ToUpper() == exitStr)
                    {
                        success = false;
                        return "";
                    }

                    if (inputStr.Length == 0)
                    {
                        success = true;
                        return "00.00.0000";
                    }

                    try
                    {
                        success = int.TryParse(inputStr.Substring(6, 4), out year);
                        success = int.TryParse(inputStr.Substring(3, 2), out month);
                        success = int.TryParse(inputStr.Substring(0, 2), out day);
                        
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
                string bankAccountInput, refNumberInput, dueDateInput, amountInput;

                // Ask user values for creating virtual bar code
                // IBAN number, loop until valid value or exit
                do
                {
                    Console.Write("Enter Finnish IBAN bank account number (X=Exit): ");
                    bankAccountInput = Console.ReadLine();

                    if (bankAccountInput.ToUpper() == "X")
                        return;

                    try
                    {
                        BankAccount myAccount = new BankAccount(bankAccountInput);
                        valid = true;
                        bankAccountInput = myAccount.IbanFormatStr.Replace(" ","");
                    }
                    catch (InvalidAccountNumberException e)
                    {
                        Console.WriteLine(e.Message);
                        valid = false;
                    }
                } while (!valid);

                // Reference number, loop until valid value or exit

                do
                {
                    refNumberInput = getRefNumber("Enter reference number (X=Exit): ", "X", out valid);

                    if (!valid)
                        return;

                    try
                    {
                        // Depending on input type, construct correct object
                        if (refNumberInput.Substring(0,2) == "RF")
                        {
                            IntReferenceNumber myRefNumber = new IntReferenceNumber(refNumberInput);
                        }
                        else
                        {
                            ReferenceNumber myRefNumber = new ReferenceNumber(refNumberInput);
                        }
                    }
                    catch (InvalidRefNumberException e)
                    {
                        Console.WriteLine(e.Message);
                        valid = false;
                    }
                } while (!valid);

                // Amount
                amountInput = getDoubleValue("Enter amount (X=Exit): ", "X", out valid, 0, 999999.99);

                if (!valid)
                    return;

                // Due date
                dueDateInput = getDateValue("Enter due date (PP.KK.VVVV): ", "X", out valid);

                if (!valid)
                    return;

                // Try to create virtual bar code based on user input
                try
                {
                    VirtualBarCode virtBarCode = new VirtualBarCode(bankAccountInput, refNumberInput, amountInput, dueDateInput);
                    Console.WriteLine();
                    Console.WriteLine("Virtual barcode is: {0}", virtBarCode.virtualBarCodeStr);
                    Console.WriteLine();
                }
                catch (InvalidVirtualBarCodeException e)
                {
                }

            } while (!exitMain);
        }
    }
}
