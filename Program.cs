using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NameFinder
{
    public class Config {
        public static int charCount;
        public static int nameCount;
    }

    class Program
    {
        static List<string> validNames = new List<string>();
        static void ConfigGet() {
            Console.WriteLine("Enter name length");
            Config.charCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount of names to check");
            Config.nameCount = Convert.ToInt32(Console.ReadLine());
        }
        static void Main(string[] args) {
            Console.WriteLine("NameFinder 0.1\nPress Enter");
            Console.ReadLine();
            if(!File.Exists("chars.txt")) {
                using (StreamWriter sw = File.CreateText("chars.txt"))
                sw.WriteLine("qwertyuiopasdfghjklzxcvbnm");
            }
            ConfigGet();
            GenerateNames();
        }

        static void GenerateNames() {
            for(int i = 0; i < Config.nameCount; i++) {
                string name = Functions.Functions.NameGen();
                if(Functions.Functions.NameCheck(name)) {
                    Console.WriteLine(name + " Is Taken");
                }
                else {
                    Console.WriteLine(name + " Is Not Taken");
                    validNames.Add(name);
                }
                Thread.Sleep(1050);
            }
            using (StreamWriter sw = File.CreateText("names.txt"))
            sw.WriteLine(String.Join("\r\n", validNames));
            Console.WriteLine("\r\n\r\nNames generated!");
            Environment.Exit(0);
        }
    }
}