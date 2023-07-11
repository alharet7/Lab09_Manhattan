using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab09_Manhattan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RootObject rootObject = RootObject.getData();
            //Output all of the neighborhoods
            RootObject.AllNeighborhoods(rootObject);
            //Filter out all the neighborhoods that do not have any names 
            RootObject.FilterEmpty(rootObject);
            //Remove the duplicates from neighborhoods
            RootObject.Duplicates(rootObject);
            //select all Neighborhood whit LINQ Query statements 
            RootObject.Neighborhood(rootObject);
        }

        public class RootObject
        {
            public string type { get; set; }
            public List<Feature> features { get; set; }
            public static RootObject getData()
            {
                string Read = File.ReadAllText("../../../data.json");
                RootObject data = JsonConvert.DeserializeObject<RootObject>(Read);

                return data;
            }

            public static void AllNeighborhoods(RootObject rootObject)
            {
                var data = rootObject.features.Select(t => t.properties);
                var allneighborhood = data.Select(n => n.neighborhood);
                foreach (var item in allneighborhood)
                {
                    Console.WriteLine(item);
                }
            }

            public static void FilterEmpty(RootObject rootObject)
            {
                var data = rootObject.features.Select(t => t.properties);
                var neighborhood = data.Where(n => n.neighborhood != "").Select(n => n.neighborhood);
                foreach (var item in neighborhood)
                {
                    Console.WriteLine(item);
                }
            }

            public static void Duplicates(RootObject rootObject)
            {
                var data = rootObject.features.Select(t => t.properties);
                var neighborhood = data.Select(n => n.neighborhood).Distinct();
                foreach (var item in neighborhood)
                {
                    Console.WriteLine(item);
                }
            }

            public static void AllQuery(RootObject rootObject)
            {
                var data = rootObject.features.Select(t => t.properties);
                var neighborhood = data.Where(n => n.neighborhood != "").Select(n => n.neighborhood).Distinct();
                foreach (var item in neighborhood)
                {
                    Console.WriteLine(item);
                }
            }

            public static void Neighborhood(RootObject rootObject)
            {
                var data = rootObject.features.Select(t => t.properties);
                var neighborhood = from properties in data
                                   select properties.neighborhood;

                foreach (var item in neighborhood)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }
        }

        public class Properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }
        }

    }
}