using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QueuePC
{
    class ServiceCommands
    {
        static public List<String> IDs;
        public static async Task DeleteFromQueueByIdAsync(string _id)  
        {
            string myJson = "{\"IndexNumber\":\"" + _id + "\"}";
            Console.WriteLine(myJson);
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(
                    "http://utpkolejka.azurewebsites.net/api/student/1",
                     new StringContent(myJson, Encoding.UTF8, "application/json"));
                Console.WriteLine(response);
            }

            /*//{"IndexNumber": "104001"}
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://utpkolejka.azurewebsites.net/api/student/1");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.GetRequestStream();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //string json = "{\"IndexNumber\": \"104001\"}";
                string json = "{\"IndexNumber\":\""+ _id +"\"}";
                Console.WriteLine(json);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }*/
        }

        public static void GetQueueStatus()
        {

        }

        public static void EditQueueStatus(string _id)
        {

        }

        public static void GetQueue(string _id)
        {
            using (var webClient = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    jsonData = webClient.DownloadString("http://utpkolejka.azurewebsites.net/api/queue/1");
                }
                catch (Exception)
                {
                }
                IDs = JsonConvert.DeserializeObject<List<String>>(jsonData);
                //Console.WriteLine(IDs.Count);
            }
        }

        public static T GetStudentDetails<T>(string _id) where T : new()
        {
            using (var webClient = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    jsonData = webClient.DownloadString("http://utpkolejka.azurewebsites.net/api/student/" + _id);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData.Substring(1, jsonData.Length - 2)) : new T();
            }
        }

    }
}
