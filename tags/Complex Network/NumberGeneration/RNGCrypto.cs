using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace NumberGeneration
{
    public class RNGCrypto
    {
        private RNGCryptoServiceProvider rng;

        public RNGCrypto()
        {
            rng = new RNGCryptoServiceProvider();
        }

        public double NextDouble()
        {
            byte[] data = new byte[4];
            rng.GetBytes(data);
            return  (double)BitConverter.ToUInt32(data, 0) / UInt32.MaxValue;
        }

        public double Next(double a, double b)
        {
            byte[] data = new byte[4];
            rng.GetBytes(data);
            return a + (b - a) / BitConverter.ToDouble(data, 0);
        }
    }
}
