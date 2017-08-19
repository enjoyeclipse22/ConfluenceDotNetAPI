# Confluence DotNet API

------

Confluence DotNet API是Confluence的DotNet实现，基于RPC/XML协议实现。
主要实现以下功能

> * 管理Confluence Login(登录操作)
> * 管理Confluence Space(空间操作
> * 管理Confluence Page (页面操作)
> * 管理Confluence Attachment(附件操作)



### . 代码实例[^code]

```C#
        private ConfluenceClient confluenceClient;
        private string token;
        private string username = "username";
        private string password = "password";
        private string confluence_url = "http://your_confluence_site/rpc/xmlrpc";

        [SetUp]
        public void SetUp()
        {
            confluenceClient = new ConfluenceClient(confluence_url);

            var authenticate = confluenceClient.Authenticate(username, password, ref token);
            Assert.IsTrue(authenticate);
        }
```


### . 相关参考
https://developer.atlassian.com/confdev/deprecated-apis/confluence-xml-rpc-and-soap-apis
