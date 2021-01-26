using Mini_Bot.service.exception;
using Mini_Bot.service.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mini_Bot.service.model
{
    public class Sushi
    {
        [JsonProperty]
        private String name;
        [JsonProperty]
        private int amount;
        [JsonProperty]
        private Double price;
        [JsonProperty]
        private String type;
        private int numberOfServings;

        public Sushi()
        { }

        public Sushi(string name, int amount, double price, string type)
        {
            this.name = name;
            this.amount = amount;
            this.price = price;
            this.type = type;
        }

        public String getName() => name;

        public void setName(String name) => this.name = name;

        public int getAmount() => amount;

        public void setAmount(int amount) => this.amount = amount;

        public Double getPrice() => price;

        public void setPrice(Double price) => this.price = price;

        public String getType() => type;

        public void setType(String type) => this.type = type;

        public int getNumberOfServings() => numberOfServings;

        public void setNumberOfServings(int numberOfServings) => this.numberOfServings = numberOfServings;

        public String toStringToMenu() => $"{getType()} | {getName()} | {getAmount()} шт. | {getPrice()} руб.";

        public String toStringToBill()
        {
            const int THE_BIGGEST_SUSHI_NAME_IN_MENU = 26;
            return $"{BotServiceUtil.getNormalizeString(getName()) + BotServiceUtil.getNCountOfSpaces(THE_BIGGEST_SUSHI_NAME_IN_MENU - BotServiceUtil.getNormalizeString(getName()).Length - 2)} " +
                $" {getPrice()}  x{getNumberOfServings()}  {getPrice() * getNumberOfServings()} руб.";
        }

        public String toStringSushisToBill(Dictionary<int, Sushi> sushis)
        {
            String str = "";
            foreach (KeyValuePair<int, Sushi> sushi in sushis)
            {
                sushi.Value.setNumberOfServings(sushi.Key);
                str += sushi.Value.toStringToBill();
                str += '\n';
            }
            return str;
        }

        public Double getSumAtAll(Dictionary<int, Sushi> sushisToOrder)
        {
            Double sum = 0;
            foreach (KeyValuePair<int, Sushi> sushi in sushisToOrder)
            {
                sum += sushi.Value.getPrice() * sushi.Key;
            }
            return sum;
        }

        public void serializeSushiInformationInJsonFile()
        {
            Sushi[] sushis = { new Sushi("НИГИРИ МАГУРО", 1, 2.90, "НИГИРИ"),
                               new Sushi("НИГИРИ ОПАЛЕННЫЙ ТУНЕЦ", 32, 50.12, "НИГИРИ"),
                               new Sushi("НИГИРИ СЯКЕ", 1, 2.94, "НИГИРИ"),
                               new Sushi("НИГИРИ ОПАЛЕННЫЙ ЛОСОСЬ", 1, 2.90, "НИГИРИ"),
                               new Sushi("НИГИРИ МАРИНОВАННЫЙ ЛОСОСЬ", 1, 3.04, "НИГИРИ"),
                               new Sushi("ТЕМАРИ МАГУРО", 1, 3.41, "ТЕМАРИ"),
                               new Sushi("ТЕМАРИ СЯКЕ", 1, 3.12, "ТЕМАРИ"),
                               new Sushi("ТЕМАРИ АВОКАДО", 1, 1.90, "ТЕМАРИ"),
                               new Sushi("ТЕМАРИ ТАЙ", 1, 2.50, "ТЕМАРИ"),
                               new Sushi("ТЕМАРИ УНАГИ", 1, 6.50, "ТЕМАРИ"),
                               new Sushi("ТЕМАРИ ЭБИ", 1, 3.50, "ТЕМАРИ"),
                               new Sushi("ГУНКАН СПАЙСИ МАГУРО", 1, 3.90, "ГУНКАНЫ"),
                               new Sushi("ГУНКАН СПАЙСИ СЯКЕ", 1, 3.90, "ГУНКАНЫ"),
                               new Sushi("ГУНКАН СПАЙСИ ТАМАГО", 1, 1.90, "ГУНКАНЫ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ КЛАССИК", 8, 16.90, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ ИКУРА", 8, 16.85, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ В КУНЖУТЕ", 4, 9.90, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ МАГУРО", 8, 16.90, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ МИНИ", 4, 12.90, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ УНАГИ", 8, 18.80, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("ФИЛАДЕЛЬФИЯ КУНСЕЙ", 8, 16.84, "ФИЛАДЕЛЬФИЯ"),
                               new Sushi("БОНИТО ТЕККА", 4, 9.41, "БОНИТО"),
                               new Sushi("БОНИТО СЯКЕ", 32, 50.12, "БОНИТО") };
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter streamWriter = new StreamWriter("menu.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(writer, sushis);
                }
            }
        }

        public Sushi getSushiByName(String name)
        {
            foreach(Sushi sushi in Menu.getDeserializedMenuFromJsonFile().getSushiList())
            {
                if (name.ToLower() == sushi.getName().ToLower())
                {
                    return sushi;
                }
            }
            throw new WrongSushiException("Таких суши нет в меню!");
        }
    }
}
