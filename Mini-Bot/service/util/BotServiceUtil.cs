using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mini_Bot.service.util
{
    class BotServiceUtil : RandomGeneratingUtil
    {
        public static String getNumbersSequenceToString(List<int> sequence)
        {
            String str = "";
            foreach (int number in sequence)
            {
                str += number;
            }
            return str;
        }

        public static String getNormalizeString(String text) => new CultureInfo("ru-RU").TextInfo.ToTitleCase(new CultureInfo("ru-RU").TextInfo.ToLower(text));

        public static String getNCountOfSpaces(int n)
        {
            String spaces = "";
            for (int counter = 0; counter < n; counter++)
            {
                spaces += " ";
            }
            return spaces;
        }
    }
}
