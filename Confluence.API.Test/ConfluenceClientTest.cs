using System;
using NUnit.Framework;
using StopWatch.Confluence;

namespace StopWatchTest
{
    [TestFixture]
    public class ConfluenceClientTest
    {
        private ConfluenceClient confluenceClient;
        private string token;
        private string username = "username";
        private string password = "password";
        private string confluence_url = "http://116.52.252.130:8090/rpc/xmlrpc";

        [SetUp]
        public void SetUp()
        {
            confluenceClient = new ConfluenceClient(confluence_url);

            var authenticate = confluenceClient.Authenticate(username, password, ref token);
            Assert.IsTrue(authenticate);
        }


        [Test]
        public void should_get_pages()
        {
            var pages = confluenceClient.GetPages(token, "information");
            foreach (var page in pages)
            {
                Console.WriteLine(page);
            }
            Assert.GreaterOrEqual(pages.Count, 1);
        }

        [Test]
        public void should_store_page()
        {
            var page = confluenceClient.StorePage(token, "TestPage4", "information", "北斗资料");
            Console.WriteLine(page);
            Assert.NotNull(page);
        }

        [Test]
        public void should_get_page()
        {
            var page = confluenceClient.GetPage(token, "information", "北斗资料");
            Console.WriteLine(page);
            Assert.NotNull(page);
        }

        [Test]
        public void should_get_pageId()
        {
            var pageId = confluenceClient.GetPageId(token, "information", "北斗资料");
            Assert.AreEqual(pageId, "950323");
        }

        [Test]
        public void should_move_page()
        {
            var sourcePageId = confluenceClient.GetPageId(token, "information", "TestPage");
            confluenceClient.MovePage(token, sourcePageId, "北斗资料", "information", "below");

        }

        [Test]
        public void should_move_pages()
        {
            var sourcePageId = confluenceClient.GetPageId(token, "information", "TestPage");
            var sourcePageId2 = confluenceClient.GetPageId(token, "information", "TestPage2");

            confluenceClient.MovePages(token, new [] {sourcePageId, sourcePageId2}, "北斗资料", "information", "below");

        }

        [Test]
        public void should_add_attachment()
        {
            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "SmallFile.pdf");
            var attachment = confluenceClient.AddAttachment(token, "information", "北斗资料", file);
            Assert.NotNull(attachment);
        }


        [Test]
        public void should_get_attachments()
        {
            var attachments = confluenceClient.GetAttachements(token, "information", "北斗资料");

            Console.WriteLine("附件数量:" + attachments.Count);
            foreach (var attachment in attachments)
            {
                Console.WriteLine(attachment);
            }
            Assert.Greater(attachments.Count, 1);
        }

        [Test]
        public void should_get_ConvertWikiToStorageFormat()
        {
            var content = confluenceClient.ConvertWikiToStorageFormat(token, "恰似一江春水向东流");

            Console.WriteLine("Content:" + content);
        }

        [Test]
        public void should_get_spaces()
        {
            var spaces = confluenceClient.GetSpaces(token);
            foreach (var space in spaces)
            {
                Console.WriteLine(space);
            }
            Assert.GreaterOrEqual(spaces.Count, 1);
        }
    }
}
