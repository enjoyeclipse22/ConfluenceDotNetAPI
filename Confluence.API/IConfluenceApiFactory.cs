namespace StopWatch.Confluence
{
    public interface IConfluenceApiFactory
    {
         IConfluenceApiRequester CreateRequest(string url);
    }
}
