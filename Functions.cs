using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using NameFinder;

namespace Functions {
    public class Functions {
        public static string NameGen() {
            Random random = new Random();
            List<string> name = new List<string>();
            for(int i = 0; i < Config.charCount; i++) {
                int number = random.Next(0, File.ReadAllText("chars.txt").Length);
                name.Add(File.ReadAllText("chars.txt").Substring(number, 1));
            }
            return(String.Join("", name));
        }
        public static bool NameCheck(string name) {
            User user;
            using (HttpClient client = new HttpClient()) {
                using (HttpResponseMessage response = client.GetAsync("https://api.mojang.com/users/profiles/minecraft/" + name).Result) {
                    using (HttpContent content = response.Content) {
                        user = JsonConvert.DeserializeObject<User>(content.ReadAsStringAsync().Result);
                    }
                }
            }
            try {
                Console.WriteLine("Checking " + user.name + "...");
                return true;
            }
            catch {
                Console.WriteLine("Checking " + name + "...");
                return false;
            }
        }
    }

    public class User {
        public string name { get; set; }
        public string id { get; set; }
        public string errorMessage { get; set; }
    }
}