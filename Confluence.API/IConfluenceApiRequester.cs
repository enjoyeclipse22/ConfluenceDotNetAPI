using CookComputing.XmlRpc;

namespace StopWatch.Confluence
{

    public interface IConfluenceApiRequester : IXmlRpcProxy
    {
        [XmlRpcMethod("confluence1.login")]
        [return: XmlRpcReturnValue(Description = "confluence1.login(): 返回两个值加法的结果;")]
        string Login(
            [XmlRpcParameter(Description = "登录账号")]  
            string username,
            [XmlRpcParameter(Description = "登录密码")]  
            string password
        );


        [XmlRpcMethod("confluence1.getSpaces")]
        [return: XmlRpcReturnValue(Description = "confluence1.getSpaces(): 返回Confluence页面;")]
        XmlRpcStruct[] GetSpaces(
            [XmlRpcParameter(Description = "token")]  
            string token

        );

        [XmlRpcMethod("confluence1.hasUser")]
        [return: XmlRpcReturnValue(Description = "confluence1.hasUser(): 返回是否有用户;")]
        string HasUser(
            [XmlRpcParameter(Description = "token")]  
            string token,
            [XmlRpcParameter(Description = "用户名")]  
            string userName
        );

        [XmlRpcMethod("confluence1.getPages")]
        [return: XmlRpcReturnValue(Description = "confluence1.getPages(): 返回某个空间下的所有页面;")]
        XmlRpcStruct[] GetPages(
            [XmlRpcParameter(Description = "token")]  
            string token,

            [XmlRpcParameter(Description = "空间名称")]  
            string space
        );

        [XmlRpcMethod("confluence1.getPageSummary")]
        [return: XmlRpcReturnValue(Description = "confluence1.getPageSummary(): 根据名字返回某个空间下某个页面;")]
        XmlRpcStruct GetPage(
            [XmlRpcParameter(Description = "token")]  
            string token,
            [XmlRpcParameter(Description = "空间名称")]  
            string space,
            [XmlRpcParameter(Description = "页面名称")]  
            string page
        );

        [XmlRpcMethod("confluence1.storePage")]
        [return: XmlRpcReturnValue(Description = "confluence1.storePage(): 上传或更新页面;")]
        XmlRpcStruct StorePage(
            [XmlRpcParameter(Description = "token")]  
            string token,

            [XmlRpcParameter(Description = "page页面信息,包括space,title,content,parent_page")]  
            XmlRpcStruct page

        );

        [XmlRpcMethod("confluence1.convertWikiToStorageFormat")]
        [return: XmlRpcReturnValue(Description = "confluence1.getAttachments(): 返回附件列表;")]
        string ConvertWikiToStorageFormat(
            [XmlRpcParameter(Description = "token")]  
            string token,

            [XmlRpcParameter(Description = "wiki markup")]  
            string markup

        );
       
        
        [XmlRpcMethod("confluence1.movePage")]
        [return: XmlRpcReturnValue(Description = "confluence1.movePage(): 移动page到另一个page下;")]
        void movePage(
            [XmlRpcParameter(Description = "token")]  
            string token,

            [XmlRpcParameter(Description = "源页面Id")]  
            string sourcePageId,
            
            [XmlRpcParameter(Description = "目标页面Id")]  
            string targetPageId,
            
            [XmlRpcParameter(Description = "append:子页面;above,上面;below，下面")]  
            string position

        );

        [XmlRpcMethod("confluence1.addAttachment")]
        [return: XmlRpcReturnValue(Description = "confluence1.getAttachments(): 返回附件列表;")]
        XmlRpcStruct AddAttachment(
            [XmlRpcParameter(Description = "token")]  
            string token,

            [XmlRpcParameter(Description = "页面Id")]  
            string pageId,

            [XmlRpcParameter(Description = "附件信息")]  
            XmlRpcStruct attachment,

            [XmlRpcParameter(Description = "附件字节")]  
            byte[] buffer
        );

        [XmlRpcMethod("confluence1.getAttachments")]
        [return: XmlRpcReturnValue(Description = "confluence1.getAttachments(): 返回附件列表;")]
        XmlRpcStruct[] GetAttachments(
            [XmlRpcParameter(Description = "token")]  
            string token,
          
            [XmlRpcParameter(Description = "页面Id")]  
            string pageId
        );


    }  
}
