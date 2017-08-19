using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch.Confluence.DTO
{
    public class Space
    {
        public string key { get; set; }

        public string name { get; set; }

        public string url { get; set; }

        public string type { get; set; }

        public override string ToString()
        {
            return "key:" + key + "\r\n" +
                   "name:" + name + "\r\n" +
                   "url:" + url + "\r\n" +
                   "type:" + type + "\r\n";
        }
    }
}
