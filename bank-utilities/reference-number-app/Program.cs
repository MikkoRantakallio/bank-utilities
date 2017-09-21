using System;
using System.Numerics;

namespace reference_number_app
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------
            // Get long value from user
            // -------------------
            long getLongValue(string msg, string exitStr, out bool success)
            {
                bool validNumber;
                long converted;
                success = true;

                // Loop until valid number given or input is equal to exit value
                do
                {
                    Console.Write("{0}: ", msg);
                    string inputStr = Console.ReadLine();

                    if (inputStr.ToUpper() == exitStr)
                    {
                        success = false;
                        return 0;
                    }

                    validNumber = long.TryParse(inputStr, out converted);

                    if (!validNumber)
                    {
                        validNumber = false;
                    }
                } while (!validNumber);
                return converted;
            }

            // --------
            // Show "menu", ask user input, loop until "X" given
            // --------
            bool exitMain = false;
            bool valid;

            do
            {
                Console.WriteLine("1 = Check reference number");
                Console.WriteLine("2 = Create reference number(s)");
                Console.WriteLine("X = Exit");
                Console.Write("Select: ");

                string menuChoice = Console.ReadLine();

                if (menuChoice.ToUpper() == "X")
                    exitMain = true;

                if (!exitMain)
                {
                    switch (menuChoice)
                    {
                        case "1":
                            long refNumber = getLongValue("Enter reference number (X=Back): ", "X", out valid);
                            break;
                        case "2":
                            long basePart = getLongValue("Enter basepart (X=Back): ", "X", out valid);
                            if (valid)
                            {
                                long refNumberCount = getLongValue("Enter count (X=Back): ", "X", out valid);
                            }
                            break;
                        default:
                            break;
                    }
                    // Check reference number
/*                    try
                    {
                        BankAccount myAccount = new BankAccount(bankAccountInput);

                        Console.WriteLine();
                        Console.WriteLine("Finnish format: \t{0}", myAccount.FinnishFormatStr);
                        Console.WriteLine("Long format: \t\t{0}", myAccount.LongFormatStr);
                        Console.WriteLine("IBAN: \t\t\t{0}", myAccount.IbanFormatStr);
                        Console.WriteLine("BIC: \t\t\t{0}", myAccount.BicStr);
                        Console.WriteLine("Check digit OK");
                    }
                    catch (InvalidAccountNumberException e)
                    {
                        Console.WriteLine(e.Message);
                    }*/
                    Console.WriteLine();
                }
            } while (!exitMain);
        }
    }
}

