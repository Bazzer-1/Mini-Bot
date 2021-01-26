using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mini_Bot.service.thread
{
    class WaitThread
    {
        private static Object locker = new Object();

        public static void wait(Object waitMessage)
        {
                Console.Write(waitMessage);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Thread.Sleep(700);
                        Console.Write(".");
                    }
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new String(' ', Console.BufferWidth));
                    Console.Write(" ");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(waitMessage);
                }
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.Write(" ");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
