using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Bot.service.util
{
    class SendMailUtil
    {
        /*public static Task SendEmailAsync(String enteredEmail)
        {
            var fr = "lobcko.artem@yandex.ru";
            var pass = "Qwerty_1234";

            MailAddress from = new MailAddress(fr, "ООО Суши Вищ");
            MailAddress to = new MailAddress(enteredEmail);
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = $"Заказ №{BotServiceUtil.getNumbersSequenceToString(RandomGeneratingUtil.numbersSequence)}";
            try
            {
                using (StreamReader streamReader = new StreamReader(Mini_Bot.service.model.ConsoleOutputMultiplexer.fileName))
                {
                   mail.Body = streamReader.ReadToEnd();
                   Console.WriteLine(mail.Body);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 25);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fr, pass);
            mail.Attachments.Add(new Attachment("counter.txt"));
            client.EnableSsl = true;           
            Console.WriteLine("Письмо отправлено");
            return client.SendMailAsync(mail);
        }*/

        public static void SendAsync(String message, String enteredEmail)
        {
            /*    // Подключите здесь службу электронной почты для отправки сообщения электронной почты.
                // настройка логина, пароля отправителя
                var from = "artempopko1232344@gmail.com";
                var pass = "Artem2222";

                //var from = "dentwo50@gmail.com";
                //var pass = "12345";

                // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(from, pass);
                client.EnableSsl = true;

                // создаем письмо: message.Destination - адрес получателя
                MailMessage mail = new MailMessage(from, enteredEmail);
                mail.Subject = $"Заказ №{BotServiceUtil.getNumbersSequenceToString(RandomGeneratingUtil.numbersSequence)}";
                mail.Body = message;
                mail.IsBodyHtml = true;
                Console.WriteLine(1);
                return client.SendMailAsync(mail);
                //return Task.FromResult(0);*/

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                mail.From = new MailAddress("lobcko.artem@yandex.ru");
                mail.To.Add(new MailAddress("killer133712@yandex.ru"));
                mail.Subject = "Test";
                mail.IsBodyHtml = true; //to make message body as html  
                mail.Body = message;
                smtp.Port = 25;
                smtp.Host = "smtp.yandex.ru"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lobcko.artem@yandex.ru", "Qwerty_1234");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
            }
            catch (Exception) { }
        }
    }
}
