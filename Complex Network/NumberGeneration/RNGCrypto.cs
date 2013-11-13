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

        public Int32 NextInt32()
        {
            byte[] data = new byte[4];
            rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public Int32 NextInt32(Int32 a, Int32 b)
        {
            byte[] data = new byte[4];
            rng.GetBytes(data);
            return Convert.ToInt32(Math.Floor((a + (b - a) / 
                (double)BitConverter.ToInt32(data, 0))));
        }

        public double NextDouble()
        {
            byte[] data = new byte[4];
            rng.GetBytes(data);
            return (double)BitConverter.ToUInt32(data, 0) / UInt32.MaxValue;
        }

        public double NextDouble(double a, double b)
        {
            byte[] data = new byte[8];
            rng.GetBytes(data);
            return a + (b - a) / BitConverter.ToDouble(data, 0);
        }
    }
}
