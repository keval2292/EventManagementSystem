using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMentorSystem.Data
{
    public static class Extensions
    {
        public static float ToDpi(this float centimeter)
        {
            var inch = centimeter / 2;
            return (float)(inch * 72);
        }
    }
}
