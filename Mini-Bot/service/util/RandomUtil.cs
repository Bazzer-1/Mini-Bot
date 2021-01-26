using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Bot.service.util
{
    class RandomUtil
    {
        private static Random random = new Random();

        public static int getRandom(int minValue, int maxValue)
        {
            return minValue + random.Next(maxValue - minValue + 1);
        }
    }
}
