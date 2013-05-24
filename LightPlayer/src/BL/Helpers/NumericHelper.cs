using System;

namespace Intems.LightPlayer.BL.Helpers
{
    public static class NumericHelper
    {
        public static byte ToByte(this Int32 int32Val)
        {
            if (int32Val < 256)
                return (byte) int32Val;

            throw new ArgumentOutOfRangeException("Can't convert to byte correctly. Argument must be less than 256");
        }

        public static byte[] ToBytes(this Int16 int16Val)
        {
            var result = new byte[sizeof(Int16)];
            result[0] = (byte)(int16Val << 8);
            result[1] = (byte)int16Val;

            return result;
        }
    }
}
