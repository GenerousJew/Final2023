using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mobile.Classes
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public static List<News> GetNews()
        {
            StreamReader reader = new StreamReader("/JSON/News.json");
            string json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<List<News>>(json);
        }
    }
}
