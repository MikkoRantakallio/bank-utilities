using System;
using System.Numerics;
using Ekoodi.Utilities.Bank;

namespace reference_number_app
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------
            // Get long value from user
            // -------------------
            string getBigIntValue(string msg, string exitStr, out bool success)
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
                    validNumber = BigInteger.TryParse(inputStr, out converted);

                } while (!validNumber);
                return inputStr;
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
                            string refNumberInput = getBigIntValue("Enter reference number (X=Back): ", "X", out valid);
                            Console.WriteLine();
                            if (valid)
                            {
                                // Try to construct a reference number object with given value
                                try
                                {
                                    ReferenceNumber refNum = new ReferenceNumber(refNumberInput);
                                    Console.WriteLine("{0} - OK", refNum.RefNumber);
                                }
                                catch (InvalidRefNumberException e)
                                {
                                    Console.WriteLine( "{0} - {1}", refNumberInput, e.Message);
                                }
                            }
                            break;
                        case "2":
                            string basePart = getBigIntValue("Enter basepart (X=Back): ", "X", out valid);
                            if (valid)
                            {
                                string refNumberCount = getBigIntValue("Enter count (X=Back): ", "X", out valid);
                                if (valid)
                                {
                                    int counter = int.Parse(refNumberCount);
                                    ReferenceNumber refNum = new ReferenceNumber();
                                    Console.WriteLine();
                                    Console.WriteLine("Generated reference numbers:");

                                    for (int i = 1; i <= counter; i++)
                                    {
                                        try
                                        {
                                            string refNumStr = refNum.GenerateRefNumber(basePart + i.ToString());
                                            Console.WriteLine("{0}. {1}", i.ToString(), refNumStr);
                                        }
                                        catch (InvalidRefNumberException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine();
                }
            } while (!exitMain);
        }
    }
}

