using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace WebApplication1.Models
{
    public class Queue
    {
        HttpClient client = new HttpClient();

        public int getStudent(int id, int q)
        {
            var position = (from p in getQueue(q)
                            where p == id.ToString()
                            select getQueue(q).IndexOf(p));
            if (position.Count() > 0)
                return position.First() + 1;
            else return 0;
        }

        public List<string> getQueue(int id)
        {
            string bufor = string.Empty;

            string query = @"https://utpkolejka.azurewebsites.net/api/queue/" + id;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(query);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                bufor = reader.ReadToEnd();
            }
            return prepareList(bufor);
        }

        private List<string> prepareList(string bufor)
        {
            bufor = new string((from c in bufor
                                where c.Equals(',') || char.IsLetterOrDigit(c)
                                select c).ToArray());
            string[] result = bufor.Split(',');
            return result.ToList<string>();
        }
    }
}