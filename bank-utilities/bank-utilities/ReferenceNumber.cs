using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Ekoodi.Utilities.ReferenceNumber
{
    class ReferenceNumber
    {
        // Properties
        public BigInteger BasePart { get; }
        public string RefNumber { get; }

        // Constructor

        public ReferenceNumber(string refNumber)
        {

        }

        public ReferenceNumber(BigInteger basePart)
        {

        }

        public string NextRefNumber()
        {
            return "FF";
        }

        public void setBasePart(BigInteger basePart)
        {

        }
    }
}
