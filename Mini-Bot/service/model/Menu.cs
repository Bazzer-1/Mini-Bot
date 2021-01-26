using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mini_Bot.service.model
{
    class Menu
    {
        private List<Sushi> sushiList;

        public Menu()
        { }

        public Menu(List<Sushi> sushiList)
        {
            this.sushiList = sushiList;
        }

        public List<Sushi> getSushiList() => sushiList;

        public void setSushiList(List<Sushi> sushis) => this.sushiList = sushis;

        public void printMenu()
        {
            Console.WriteLine("\t\t\tМеню:");
            foreach (Sushi sushi in getSushiList())
            {
                Console.WriteLine(sushi.toStringToMenu());
            }
        }

        public static Menu getDeserializedMenuFromJsonFile()
        {
            return new Menu(JsonConvert.DeserializeObject<List<Sushi>>(File.ReadAllText(@"menu.json")));
        }
    }
}
