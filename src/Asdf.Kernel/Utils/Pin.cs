using System;

namespace Asdf.Kernel.Utils
{
    public static class Pin
    {
        public static int Generate()
        {
            var random = new Random(DateTime.Now.Millisecond);
            return random.Next(minValue: 1000, maxValue: 9999);
        }
    }
}
