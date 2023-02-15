using System;

namespace FlagCapturing.Utils
{
    public static class Helpers
    {
        public static bool InRange(in float value, in float leftBorder, in float rightBorder)
        {
            return value >= leftBorder && value <= rightBorder;
        }

        public static int FloatToMilliseconds(in float value)
        {
            return (int)Math.Floor(value * 1000f);
        }
    }
}
