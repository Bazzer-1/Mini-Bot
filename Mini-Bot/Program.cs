using Mini_Bot.service;
using Mini_Bot.service.impl;
using System;

namespace Mini_Bot
{
    public class Program
    {
        public static void Main(String[] args)
        {
            BotService botService = new BotServiceImpl();
            botService.runBotAsync();
        }
    }
}
