using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch.Confluence.DTO
{
    public class Attachement
    {
        public string id { get; set; }

        public string pageId { get; set; }

        public string title { get; set; }

        public string fileName { get; set; }

        public string fileSize { get; set; }

        public string contentType { get; set; }

        public string url { get; set; }

        public string creator { get; set; }

        public DateTime created { get; set; }
        public override string ToString()
        {
            return "id:" + id + "\r\n" +
                   "pageid:" + pageId + "\r\n" +
                   " title:" + title + "\r\n" +
                   " fileName:" + fileName + "\r\n" +
                   " fileSize:" + fileSize + "\r\n" +
                   " contentType:" + contentType + "\r\n" +
                   " url:" + url + "\r\n" +
                   " creator:" + creator + "\r\n" +
                   " created:" + created + "\r\n";
        }
    }
}
