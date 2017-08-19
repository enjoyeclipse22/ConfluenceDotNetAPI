/**************************************************************************
Copyright 2016 Carsten Gehling

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using StopWatch.Confluence.DTO;
using StopWatch.Helpers;

namespace StopWatch.Confluence
{
    public class ConfluenceClient
    {
        public bool SessionValid { get; private set; }
        public string ErrorMessage { get; private set; }
        public string BaseUrl { get; private set; }

        private readonly IConfluenceApiFactory _apiFactory;
        private readonly IConfluenceApiRequester _apiRequester;

        public ConfluenceClient(string url)
        {
            _apiFactory = new ConfluenceApiFactory(); 
            SessionValid = false;
            ErrorMessage = string.Empty;
            BaseUrl = url;
            _apiRequester = _apiFactory.CreateRequest(BaseUrl);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Authenticate(string username, string password, ref string token)
        {
            SessionValid = false;

            try
            {
                token = _apiRequester.Login(username, password);
                return true;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return false;
            }
        }

        /// <summary>
        /// 获某空间下所有页面
        /// </summary>
        /// <param name="token"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public List<Page> GetPages(string token, string space)
        {
            try
            {
                var results = _apiRequester.GetPages(token, space);
                var pages = results.Convert<Page>();
                return pages;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return new List<Page>();
            }
        }

        /// <summary>
        /// 获取某用户的所有空间
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public List<Space> GetSpaces(string token)
        {
            try
            {
                var results = _apiRequester.GetSpaces(token);
                var spaces = results.Convert<Space>();
                return spaces;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return new List<Space>();
            }
        }

        /// <summary>
        /// 添加或更新页面
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pageName"></param>
        /// <param name="spaceKey"></param>
        /// <param name="parentPageName"></param>
        /// <param name="content"></param>
        /// <param name="convert_wiki"></param>
        /// <returns></returns>
        public Page StorePage(string token, string pageName, string spaceKey, string parentPageName, string content = "",
            bool convert_wiki = true)
        {
            try
            {
                var page = GetPage(token, spaceKey, pageName);

                //页面不存在
                if (page == null)
                {
                    page = new Page
                    {
                        space = spaceKey, title = pageName
                    };
                }

                if (!string.IsNullOrWhiteSpace(parentPageName))
                {
                    var parentPageId = GetPageId(token, spaceKey, parentPageName);
                    page.parentId = parentPageId;
                }

                if (convert_wiki)
                {
                    content = ConvertWikiToStorageFormat(token, content);
                }
                page.content = content;
                var rpcEntity = page.Convert();
                var result = _apiRequester.StorePage(token, rpcEntity);
                return result.Convert<Page>();
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return null;
            }
        }
        /// <summary>
        /// 移动某个页面
        /// </summary>
        /// <param name="token"></param>
        /// <param name="sourcePageId"></param>
        /// <param name="targetPageName"></param>
        /// <param name="spaceKey"></param>
        /// <param name="position"></param>
        public void MovePage(string token, string sourcePageId, string targetPageName, string spaceKey, string position = "below")
        {
            try
            {
                var targetPageId = GetPageId(token, spaceKey, targetPageName);
                _apiRequester.movePage(token, sourcePageId, targetPageId, position);
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;

            }
        }

        /// <summary>
        /// 批量移动几个页面
        /// </summary>
        /// <param name="token"></param>
        /// <param name="sourcePageIds"></param>
        /// <param name="targetPageName"></param>
        /// <param name="spaceKey"></param>
        /// <param name="position"></param>
        public void MovePages(string token, string[] sourcePageIds, string targetPageName, string spaceKey, string position="below")
        {
            try
            {
                var targetPageId = GetPageId(token, spaceKey, targetPageName);
                foreach (var pageId in sourcePageIds)
                {
                    _apiRequester.movePage(token, pageId, targetPageId, position);  
                }
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
          
            }
        }

        /// <summary>
        /// 获取某个页面信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="spaceKey"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public Page GetPage(string token, string spaceKey, string pageName)
        {
            try
            {
                var result = _apiRequester.GetPage(token, spaceKey, pageName);
                return result.Convert<Page>();
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取页面Id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="spaceKey"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public string GetPageId(string token, string spaceKey, string pageName)
        {
            try
            {
                var result = _apiRequester.GetPage(token, spaceKey, pageName);
                return result.Convert<Page>().id;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="spaceKey"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public List<Attachement> GetAttachements(string token, string spaceKey, string pageName)
        {
            try
            {
                var pageId = GetPageId(token, spaceKey, pageName);
                return GetAttachements(token, pageId);
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return new List<Attachement>();
            }
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="token"></param>
        /// <param name="spaceKey"></param>
        /// <param name="pageName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public Attachement AddAttachment(string token, string spaceKey, string pageName, string file)
        {
            try
            {
                var page = GetPage(token, spaceKey, pageName);
                if (page == null)
                {
                    throw new Exception("页面不存在");
                }
                
                var attchment = new Attachement
                {
                    contentType = Mime.GetMimeType(file),
                    title = Path.GetFileName(file),
                    fileName = Path.GetFileName(file),
                    created = DateTime.Now,
                };
                var buffer = File.ReadAllBytes(file);
                var rpcStruct = attchment.Convert();
                var result = _apiRequester.AddAttachment(token, page.id, rpcStruct, buffer);
                return result.Convert<Attachement>();
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据页面Id获取附件列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public List<Attachement> GetAttachements(string token, string pageId)
        {
            try
            {
                var result = _apiRequester.GetAttachments(token, pageId);
                return result.Convert<Attachement>();
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return new List<Attachement>();
            }
        }


        /// <summary>
        /// 转换wiki格式为HTML/XML格式
        /// </summary>
        /// <param name="token"></param>
        /// <param name="markup"></param>
        /// <returns></returns>
        public string ConvertWikiToStorageFormat(string token, string markup)
        {
            try
            {
                return _apiRequester.ConvertWikiToStorageFormat(token, markup);
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return string.Empty;
            }
        }

        public bool ValidateSession()
        {
            SessionValid = false;

            //var request = _apiFactory.CreateValidateSessionRequest();
            try
            {
               // _confluenceApiRequester.DoAuthenticatedRequest<object>(request);
                SessionValid = true;
                return true;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                return false;
            }
        }


        
    }
}
