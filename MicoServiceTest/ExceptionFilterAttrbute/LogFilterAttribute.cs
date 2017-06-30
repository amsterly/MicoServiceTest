/*
 * 
 * 名称：LogFilterAttribute
 * 
 * 描述：记录日志，异常处理
 * 
 * 创建人：zhaopeng
 * 
 * 创建日期：2016.6.23
 * 
 * 修改人：
 * 
 * 修改日期： 
 * 
 * 版权：同方威视
 * 
 * 版本号：1.0.0.0
 * 
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;

namespace MicoServiceTest.ExceptionFilterAttrbute
{
    public class LogFilterAttribute:ActionFilterAttribute
    {
        public readonly ILog _log = LogManager.GetLogger(Log.DefaultLogName);

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request == null)
                return;

            try
            {
                var logString = LogFormat(actionExecutedContext);

                if (actionExecutedContext.Exception == null)
                {
                    _log.Info(logString);
                    return;
                }

                logString += " " + actionExecutedContext.Exception.Message;
                _log.Error(logString);
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
            }
        }

        private string LogFormat(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext == null)
                return string.Empty;

            if (actionExecutedContext.Request == null)
                return string.Empty;

            var startTime = DateTime.Now;
            var builder = new StringBuilder();
            builder.Append("[");
            builder.Append(actionExecutedContext.ActionContext.ControllerContext.Controller);
            builder.Append("]");
            builder.Append("[");
            builder.Append(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            builder.Append("]");
            builder.Append(" receive request ");
            builder.Append((DateTime.Now - startTime).TotalMilliseconds/1000 + " seconds ");
            FormatArguments(builder, actionExecutedContext.ActionContext.ActionArguments);
            builder.Append(actionExecutedContext.Request.RequestUri);

            return builder.ToString();
        }

        private void FormatArguments(StringBuilder stringBuilder,Dictionary<string, object> actionArguments)
        {
            if (actionArguments == null)
                return;

            foreach (KeyValuePair<string, object> pair in actionArguments)
            {
                stringBuilder.Append(pair.Key + ":" + pair.Value+",");
            }
        }
    }
}
