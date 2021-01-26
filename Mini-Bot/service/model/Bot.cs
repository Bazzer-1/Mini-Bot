using Mini_Bot.service.exception;
using Mini_Bot.service.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mini_Bot.service.model
{
    class Bot
    {
        private static String message;
        private RandomGeneratingUtil randomGeneratingUtil = new RandomGeneratingUtil();
        private const String companyName = "Суши Вищ";
        private const String undegroundName = "Фрунзенская";

        public Bot()
        {}

        public Bot(String message)
        {
            Bot.message = message;
        }

        public String getMessage() => message;

        public void setMessage(String message) => Bot.message = message; 

        public void welcomeMessage()
        {
            setMessage("Добрый день, как Вас зовут?");
            Console.WriteLine(getMessage());
        }

        public void offerMenuMessage(String name)
        {
            setMessage($"Здравствуйте, {name}, хотите меню?");
            Console.WriteLine(getMessage());
        }

        public void chooseOfferMessage(String name)
        {
            String answer;
            do
            {
                setMessage($"Выбрали что-нибудь, {name}?");
                System.Console.WriteLine(getMessage());
                answer = System.Console.ReadLine();
                try
                {
                    checkingForCorrectAnswer(answer);
                }
                catch (WrongAnswerException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                if (!checkingForPositiveAnswer(answer))
                {
                    Thread.Sleep(2000);
                }
            } while (!checkingForPositiveAnswer(answer));
        }

        public void enterSushiNameMessage()
        {
            setMessage("Введите название суши для заказа: ");
            Console.WriteLine(getMessage());
        }

        public void enterCountOfServingsMessage()
        {
            setMessage("Введите кол-во порций для заказа: ");
            Console.WriteLine(getMessage());
        }

        public void menuSushiMessage()
        {
            setMessage("Вот ваше меню");
            Console.WriteLine(getMessage());
        }

        public async System.Threading.Tasks.Task enterEmailMessageAsync()
        {
            string email = "";
            setMessage("Хотите ли ввести email для отправки на него чека с номером заказа");
            Console.WriteLine(getMessage());
            String answer = System.Console.ReadLine();
            try
            {
                checkingForCorrectAnswer(answer);
            }
            catch (WrongAnswerException e)
            {
                Console.WriteLine(e.Message);
            }
            if (checkingForPositiveAnswer(answer))
            {
                string pattern = @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$";
                while (true)
                {
                    setMessage("Введите email: ");
                    Console.WriteLine(getMessage());
                    email = Console.ReadLine();

                    if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                    {
                        String message = "";
                        setMessage("Email подтвержден");
                        Console.WriteLine(getMessage());
                        try
                        {
                            using (StreamReader streamReader = new StreamReader(Mini_Bot.service.model.ConsoleOutputMultiplexer.fileName))
                            {
                                message = streamReader.ReadToEnd();
                            }
                            SendMailUtil.SendAsync(message, email);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    else
                    {
                        setMessage("Некорректный email");
                        Console.WriteLine(getMessage());
                    }
                }
            }
        }

        public void chooseOfferMessageTwiceOrMore(Dictionary<int, Sushi> productsToOrder)
        {
            Sushi sushi = new Sushi();
            String answer;
            do
            {
                System.Console.WriteLine($"Хотите что-нибудь ещё?");
                answer = System.Console.ReadLine();
                try
                {
                   checkingForCorrectAnswer(answer);
                }
                catch (WrongAnswerException e)
                {
                    Console.WriteLine(e.Message);
                }
                if (checkingForPositiveAnswer(answer))
                {
                    Thread.Sleep(2000);
                    enterSushiNameMessage();
                    String nameOfSushi = System.Console.ReadLine();
                    enterCountOfServingsMessage();
                    int countOfServings;
                    do
                    {
                        countOfServings = int.Parse(Console.ReadLine());
                    } while (countOfServings <= 0 || countOfServings >= 100);
                    try
                    {
                        productsToOrder.Add(countOfServings, sushi.getSushiByName(nameOfSushi));
                    }
                    catch (WrongSushiException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            } while (checkingForPositiveAnswer(answer));
        }

        public Boolean checkingForCorrectAnswer(String answer)
        {
            if (answer.ToLower() == "да" || answer.ToLower() == "нет")
                return true;
            else
                throw new WrongAnswerException("Некорректный ввод!");
        }

        public Boolean checkingForPositiveAnswer(String answer) => answer.ToLower() == "да";

        public void printBillMessage(Dictionary<int, Sushi> sushisToOrder)
        {
            const int COUNT_OF_NUMBERS = 5;
            for (int counter = 0; counter < COUNT_OF_NUMBERS; counter++)
            {
                RandomGeneratingUtil.numbersSequence.Add(randomGeneratingUtil.getRandomNumber());
            }
            /*TextWriter oldOut = Console.Out;
            TextWriter writer;
            FileStream ostrm;
            try
            {
                ostrm = new FileStream("output.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            randomGeneratingUtil.printRandomNumbersImage();
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();*/
            using (ConsoleOutputMultiplexer co = new ConsoleOutputMultiplexer())
            {
                setMessage("Заказ принят!\n" +
                       "Ваш чек\n\n" +
                       $"\t ООО {companyName}\n" +
                       $"\t    {companyName}\n" +
                       $"\tст. м. {undegroundName}\n\n" +
                       $"Время печати: {DateTime.Now}\n" +
                       "Стол:\t\t     ТИП! Общие\n" +
                       "Ваш номер заказа в очереди:\n" +
                       $"{BotServiceUtil.getNumbersSequenceToString(RandomGeneratingUtil.numbersSequence)}\n");
                RandomGeneratingUtil.setCountOfLinesInTextBeforeImage(randomGeneratingUtil.getCountOfLinesInText(getMessage()));
                Console.WriteLine(getMessage());
                randomGeneratingUtil.printRandomNumbersImage();
                clearMessage();
                Sushi sushi = new Sushi();
                const int THE_BIGGEST_SUSHI_NAME_IN_MENU = 26;
                setMessage($"Блюдо {BotServiceUtil.getNCountOfSpaces(THE_BIGGEST_SUSHI_NAME_IN_MENU - 10)}| Цена | Кол-во | Сумма\n" +
                           "----------------------------------------\n" +
                           $"{sushi.toStringSushisToBill(sushisToOrder)}\n\n" +
                           "----------------------------------------\n\n" +
                           $"Всего                          {sushi.getSumAtAll(sushisToOrder)} руб.\n\n" +
                           "----------------------------------------");
                Console.WriteLine(getMessage());
            }
        }

        public void clearMessage()
        {
            setMessage("");
        }
    }
}
