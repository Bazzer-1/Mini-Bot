using Mini_Bot.service.exception;
using Mini_Bot.service.model;
using Mini_Bot.service.thread;
using Mini_Bot.service.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mini_Bot.service.impl
{
    class BotServiceImpl : BotService
    {
        private Bot bot = new Bot();
        private Sushi sushi = new Sushi();
        Thread waitDefaultThread = new Thread(new ParameterizedThreadStart(WaitThread.wait));
        Thread waitBillThread = new Thread(new ParameterizedThreadStart(WaitThread.wait));

        private static String waitDefaultMessage = "Ожидайте";
        private static String waitBillMessage = "Печать чека";

        public async void runBotAsync()
        {
            Dictionary<int, Sushi> sushisToOrder = new Dictionary<int, Sushi>();
            sushi.serializeSushiInformationInJsonFile();
            bot.welcomeMessage();
            String name = Console.ReadLine();
            bot.offerMenuMessage(name);
            //Console.WriteLine($"{name}, скажите что-нибудь");
            String answer = Console.ReadLine();
            try
            {
                bot.checkingForCorrectAnswer(answer);
            }
            catch (WrongAnswerException e)
            {
                Console.WriteLine(e.Message);
            }
            if (bot.checkingForPositiveAnswer(answer))
            {
                waitDefaultThread.Start(waitDefaultMessage); // запускаем поток
                waitDefaultThread.Join();
                Console.Clear();
                bot.menuSushiMessage();
                Menu.getDeserializedMenuFromJsonFile().printMenu();
                bot.chooseOfferMessage(name);
                bot.enterSushiNameMessage();
                String nameOfSushi = Console.ReadLine();
                bot.enterCountOfServingsMessage();
                int countOfServings;
                do
                {
                    countOfServings = int.Parse(Console.ReadLine());
                } while (countOfServings <= 0 || countOfServings >= 100);
                try
                {
                    sushisToOrder.Add(countOfServings, sushi.getSushiByName(nameOfSushi));
                }
                catch (WrongSushiException e)
                {
                    Console.WriteLine(e.Message);
                }
                bot.chooseOfferMessageTwiceOrMore(sushisToOrder);
                Console.Clear();
                waitBillThread.Start(waitBillMessage); // запускаем поток
                waitBillThread.Join();
                /*foreach (Sushi sushiToOrder in sushisToOrder)
                {
                    Console.WriteLine(sushiToOrder.toString());
                }*/
                bot.printBillMessage(sushisToOrder);
                await bot.enterEmailMessageAsync();
            }
        }
    }
}
