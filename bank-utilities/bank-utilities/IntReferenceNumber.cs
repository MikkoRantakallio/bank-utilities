using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Ekoodi.Utilities.Bank
{
    public class IntReferenceNumber: ReferenceNumber
    {
        //-------
        // Properties
        //-------
        public string RefNumberINT { get; set; }
        public string BasePartINT { get; set; }

        //-------
        // Constructors
        //-------

        // Construct the base class with Finnish reference number
        public IntReferenceNumber(string intRefNum): base (intRefNum.Substring(4))
        {
            CheckIntRefNum(intRefNum);

            RefNumberINT = intRefNum.ToUpper();
            BasePartINT = intRefNum.Substring(4);
        }

        public IntReferenceNumber()
        {
        }

        //-------
        // Methods
        //-------

        public void CheckFinnishPart(string refPartFI)
        {
            base.CheckRefNum(refPartFI);
            BasePartINT = refPartFI;
        }

        public string GenerateIntRefNumber()
        {
            int checkNumber = CalculateIntCheckNumber(BasePartINT);
            RefNumberINT = "RF" + checkNumber.ToString("00") + BasePartINT;
            return RefNumberINT;
        }

        // Calculate international check number, return -1 if not successful
        private int CalculateIntCheckNumber(string refNumFI)
        {
            string finalString = refNumFI + "271500";
            BigInteger x;

            bool ok = BigInteger.TryParse(finalString, out x);

            if (ok) {
                BigInteger checkNumber = 98 - (x % 97);

                return (int)checkNumber;
            }
            else
            {
                return -1;
            }
        }

        // Check international reference number
        public void CheckIntRefNum(string intRefNum)
        {
            string letterPart = intRefNum.Substring(0, 2);
            string intRefPart = intRefNum.Substring(2, 2);
            string refPartFI = intRefNum.Substring(4);

            int intRefCheck;
            bool checkPartOK = int.TryParse(intRefPart, out intRefCheck);

            // Letter prefix is not correct
            if (letterPart.ToUpper() != "RF")
            {
                throw new InvalidRefNumberException("\"RF\" invalid or missing!");
            }

            // Check Finnish reference number
            int calculatedIntCheckNumber = CalculateIntCheckNumber(refPartFI);

            if (calculatedIntCheckNumber !=  intRefCheck)
            {
                string msg = "Invalid international check number! (" + calculatedIntCheckNumber + ")";
                throw new InvalidRefNumberException(msg);
            }
        }
    }
}
