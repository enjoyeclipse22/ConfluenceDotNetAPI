using System.Text;
using CookComputing.XmlRpc;

namespace StopWatch.Confluence
{
    public class ConfluenceApiFactory : IConfluenceApiFactory
    {
        public IConfluenceApiRequester CreateRequest(string url)
        {
            var xmlRpcProxy = XmlRpcProxyGen.Create<IConfluenceApiRequester>();
            xmlRpcProxy.XmlEncoding = new UTF8Encoding();
            xmlRpcProxy.Url = url;
            return xmlRpcProxy;
        }
    }
}
