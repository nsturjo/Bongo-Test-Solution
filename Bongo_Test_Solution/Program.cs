using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSON
{
    class Program
    {
        // global variable for storing the result set
        public static List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
        
        //sample test JSON in string
        public static string sampleJSON = @"{
	                                         ""key1"": ""1"",
                                             ""key2"": {
                                                        ""key3"": ""1"",
		                                                ""key4"": {
                                                                   ""key5"": ""4""
                                                                  }
                                                       }
                                            }";


        static void Main(string[] args)
        {
            Solution1();
            Solution2();
           
            Console.WriteLine("\n\nPress enter to close...");
            Console.ReadLine();

        }

        public static void Solution1()
        {
            result.Clear();

            var details = JObject.Parse(sampleJSON);

            // calling method to add key, depth in result
            // having sample Json and default level as parameter
            KeyDepth(details, 1);

            Console.WriteLine("Output for Solution 1:");
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + ' ' + item.Value);
            }
        }

        public static void Solution2()
        {
            result.Clear();

            Person person_a = new Person("User", "1", "none");
            Person person_b = new Person("User", "1", person_a);

            var details = JObject.Parse(sampleJSON);

            // to construct the sample JSON data 
            JObject channel1 = (JObject)details["key2"];
            JObject channel2 = (JObject)channel1["key4"];
            channel2.Property("key5").AddAfterSelf(new JProperty("user", JObject.Parse(JsonConvert.SerializeObject(person_b))));

            KeyDepth(details, 1);

            Console.WriteLine("\n\nOutput for Solution 2:");
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + ' ' + item.Value);
            }
        }

        // Recursive method to determine the depth of the key
        public static void KeyDepth(JObject obj, int level)
        {
            foreach (var item in obj)
            {
                string typo = item.Value.Type.ToString();
                
                // type object means the item contains nested element
                if (typo == "Object")
                {
                    result.Add(new KeyValuePair<string, int>(item.Key, level));
                    KeyDepth(JObject.Parse(item.Value.ToString()), level + 1);
                }
                else
                    // adding the key and depth to dictionary
                    result.Add(new KeyValuePair<string, int>(item.Key, level));
            }
        }

        //given sample class 
        [Serializable()]
        class Person : ISerializable
        {
            string _first_name, _last_name;
            object _father;
            public Person(string first_name, string last_name, object father)
            {
                this._first_name = first_name;
                this._last_name = last_name;
                this._father = father;
            }

            // method to enable Custome Serialization
            public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
            {
                info.AddValue("first_name:", _first_name);
                info.AddValue("last_name:", _last_name);
                info.AddValue("father:", _father);
            }
        }
    }
}
