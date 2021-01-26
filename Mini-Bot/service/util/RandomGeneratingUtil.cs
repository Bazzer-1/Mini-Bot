using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mini_Bot.service.util
{
    class RandomGeneratingUtil
    {
        private const string zero = 
               "$$$$$$\n" +
               "$    $\n" +
               "$    $\n" +
               "$    $\n" +
               "$    $\n" +
               "$    $\n" +
               "$$$$$$\n";

        private const string one = "" +
                "  $   \n" +
                " $$   \n" +
                "  $\n" +
                "  $\n" +
                "  $\n" +
                "  $\n" +
                "$$$$$\n";

        private const string two = "" +
                " $$$\n" +
                "$   $\n" +
                "    $\n" +
                "   $\n" +
                "  $\n" +
                " $\n" +
                "$$$$$$\n";

        private const string three = "" +
                "$$$$$$\n" +
                "$    $\n" +
                "     $\n" +
                "$$$$$$\n" +
                "     $\n" +
                "$    $\n" +
                "$$$$$$\n";

        private const string four = "" +
                "$    $\n" +
                "$    $\n" +
                "$    $\n" +
                "$$$$$$\n" +
                "     $\n" +
                "     $\n" +
                "     $\n";

        private const string five = "" +
                "$$$$$$\n" +
                "$\n" +
                "$\n" +
                "$$$$$$\n" +
                "     $\n" +
                "     $\n" +
                "$$$$$$\n";

        private const string six = "" +
                "$$$$$$\n" +
                "$\n" +
                "$\n" +
                "$$$$$$\n" +
                "$    $\n" +
                "$    $\n" +
                "$$$$$$\n";

        private const string seven = "" +
               "$$$$$$\n" +
               "     $\n" +
               "    $\n" +
               "   $\n" +
               "   $\n" +
               "   $\n" +
               "   $\n";

        private const string eight = "" +
          "$$$$$$\n" +
          "$    $\n" +
          "$    $\n" +
          "$$$$$$\n" +
          "$    $\n" +
          "$    $\n" +
          "$$$$$$\n";

        private const string nine = "" +
          "$$$$$$\n" +
          "$    $\n" +
          "$    $\n" +
          "$$$$$$\n" +
          "     $\n" +
          "     $\n" +
          "$$$$$$\n";

        private static int countOfLinesInTextBeforeImage;

        public static void setCountOfLinesInTextBeforeImage(int countOfLinesInTextBeforeImage) => RandomGeneratingUtil.countOfLinesInTextBeforeImage = countOfLinesInTextBeforeImage;

        private static int numberToPrint;

        public static int getNumberToPrint => numberToPrint;

        public static List<int> numbersSequence = new List<int>();

        public void printRandomNumbersImage()
        {
            List<String> numbers = new List<String>
            {
                zero,
                one,
                two,
                three,
                four,
                five,
                six,
                seven,
                eight,
                nine
            };

            const int COUNT_OF_PASSING_PIXELS_BEFORE_FIRST_NUMBER_IMAGE = 2;
            int HEIGHT_LENGTH_OF_NUMBER = getCountOfLinesInText(zero);
            int widthCounter = COUNT_OF_PASSING_PIXELS_BEFORE_FIRST_NUMBER_IMAGE;
            for (int counter = 0; counter < numbersSequence.Count; counter++)
            {
                List<String> number = getTextByLines(numbers[numbersSequence[counter]]);
                for (int numberHeightCounter = 0, topCounter = countOfLinesInTextBeforeImage; numberHeightCounter < HEIGHT_LENGTH_OF_NUMBER; numberHeightCounter++, topCounter++)
                {
                    Console.SetCursorPosition(widthCounter, topCounter);
                    Console.WriteLine(number[numberHeightCounter]);                    
                }
                widthCounter += HEIGHT_LENGTH_OF_NUMBER;
            }
        }

        public int getRandomNumber()
        {
            const int MIN_VALUE_OF_NUMBER = 0;
            const int MAX_VALUE_OF_NUMBER = 9;
            return RandomUtil.getRandom(MIN_VALUE_OF_NUMBER, MAX_VALUE_OF_NUMBER);
        }

        protected List<String> getTextByLines(String text)
        {
            return text.Split('\n').Cast<String>().ToList();
        }

        public int getCountOfLinesInText(String text) => getTextByLines(text).Count; 
    }
}
