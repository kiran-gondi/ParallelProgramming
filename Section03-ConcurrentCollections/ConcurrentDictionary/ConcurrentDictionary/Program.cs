using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace ConcurrentDictionaryDemo
{
    internal class Program
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");

            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main Thread";
            Console.WriteLine($"{who} {(success? "added" : "did not add")} the element");
        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            //capitals["Russia"] = "Leningrad";
            //capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + " --> Moscow" );
            //Console.WriteLine($"The capital of Russia is {capitals["Russia"]}");

            //capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capOfSweden}");

            const string toRemove = "Russia";
            string removed;
            var didRemoved = capitals.TryRemove(toRemove, out removed);
            if (didRemoved)
            {
                Console.WriteLine($"Capital removed is {toRemove} {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove the capital of {toRemove}");
            }


            //Expensive operation of Enumerating the ConcurrentDictionary
            //Count on ConcurrentDictionary is also an expensive operation
            foreach (var kvp in capitals) { 
            Console.WriteLine($"{kvp.Value} is the capital of {kvp.Key}");
            }

            Console.WriteLine("End of Main!");
            Console.ReadKey();
        }
    }
}
