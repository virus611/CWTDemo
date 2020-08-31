using CWTDemo.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CWTDemo.Lib
{
    public class CWTFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }


        #region ActionFilterAttribute 成员
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            #region 白名单部分,设置部分路由不参与拦截
            string s = filterContext.HttpContext.Request.Path.Value; 
            if (s.StartsWith("/Login"))
            {
                return;
            } 
            #endregion

            if (string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.Headers["UID"]))
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            string str = filterContext.HttpContext.Request.Headers["UID"].ToString();
            try
            {
                string m2 = AESTools.AESDecrypt(str).Replace("\0", "");
                if (string.IsNullOrWhiteSpace(m2))
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
                CWTUserModel obj = JsonSerializer.Deserialize<CWTUserModel>(m2);
                if (obj == null || obj.Expired < DateTime.Now)
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
            }
            catch
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }
        }
        #endregion
    }


}
