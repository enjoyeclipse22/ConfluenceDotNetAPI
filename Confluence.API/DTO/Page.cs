using System;

namespace StopWatch.Confluence.DTO
{
    public class Page
    {
        /// <summary>
        /// 页面Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 空间名称
        /// </summary>
        public string space { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 页面标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 页面Url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 页面版本version
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 页面Content
        /// </summary>
        public string permissions { get; set; }

        /// <summary>
        /// 页面内容
        /// </summary>
        public string content { get; set; }

        public override string ToString()
        {
            return "id:" + id + "\r\n" + 
                   "space:" + space + "\r\n" +
                   "parentId:" + parentId + "\r\n" +
                   " title:" + title + "\r\n" +
                   " url:" + url + "\r\n" + 
                   " version:" + version + "\r\n"
                   + " permissions:" + permissions + "\r\n";
        }
    }
}
