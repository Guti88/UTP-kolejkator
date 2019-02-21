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
        /// <summary>
        /// Stores ID's of student in the queue
        /// </summary>
        static public List<String> IDs;
        /// <summary>
        /// Stores statuses of the queues
        /// </summary>
        static public List<Queue> queueStatuses;

        /// <summary>
        /// Deletes student with a given ID
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static async Task DeleteFromQueueByIdAsync(string _id)  
        {
            try
            {
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent("{\"IndexNumber\":\"" + _id + "\"}", Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("http://utpkolejka.azurewebsites.net/api/student/1")
                };
                await httpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: "+ e.Message, "There was an error when deleting student from the queue", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets statues of all queues
        /// </summary>
        public static void GetQueueStatus()
        {
            using (var webClient = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    jsonData = webClient.DownloadString("https://utpkolejka.azurewebsites.net/api/queueinfo");
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Error: " + e.Message, "There was an error when receiving queue status", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                queueStatuses = JsonConvert.DeserializeObject<List<Queue>>(jsonData);
            }
        }

        /// <summary>
        /// Edits status of given queue
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_status"></param>
        /// <returns></returns>
        public static async Task EditQueueStatus(string _id, string _status)
        {
            try
            {
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent("{\"idQueue\":\"" + _id + "\",\"status\":\"" +_status + "\"}", Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("http://utpkolejka.azurewebsites.net/api/queueinfo")
                };
                await httpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: " + e.Message, "There was an error when trying to change queue status", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets ID's from given queue
        /// </summary>
        /// <param name="_id"></param>
        public static void GetQueue(string _id)
        {
            using (var webClient = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    jsonData = webClient.DownloadString("http://utpkolejka.azurewebsites.net/api/queue/1");
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Error: " + e.Message, "There was an error when fetching queue", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                IDs = JsonConvert.DeserializeObject<List<String>>(jsonData);
            }
        }

        /// <summary>
        /// Gets student details
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static T GetStudentDetails<T>(string _id) where T : new()
        {
            using (var webClient = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    if(_id != null)
                    {
                        jsonData = webClient.DownloadString("http://utpkolejka.azurewebsites.net/api/student/" + _id);
                    } 
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Error: " + e.Message, "There was an error when fetching student data", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData.Substring(1, jsonData.Length - 2)) : new T();
            }
        }

    }
}
