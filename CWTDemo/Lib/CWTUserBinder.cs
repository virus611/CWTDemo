using CWTDemo.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CWTDemo.Lib
{
    public class CWTUserBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(CWTUser))
            {
                return new BinderTypeModelBinder(typeof(CWTUserBinder));
            }

            return null;
        }
    }

    public class CWTUserBinder : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (string.IsNullOrWhiteSpace(bindingContext.HttpContext.Request.Headers["UID"]))
            {
                return Task.CompletedTask;
            }
            else
            {
                string str = bindingContext.HttpContext.Request.Headers["UID"].ToString();
                try
                {
                    string m2 = AESTools.AESDecrypt(str).Replace("\0", "");
                    if (string.IsNullOrWhiteSpace(m2))
                    {
                        return Task.CompletedTask;
                    }
                    CWTUserModel obj = JsonSerializer.Deserialize<CWTUserModel>(m2);
                    if (obj == null || obj.Expired < DateTime.Now)
                    {
                        return Task.CompletedTask;
                    }
                    //将obj的部分信息映射成供controller可用的数据，可以从redis中拿数据
                    //不建议从db中获取数据，有性能问题
                    CWTUser re = new CWTUser()
                    {
                        UID = obj.UID
                    };

                    //合法 
                    bindingContext.Result = ModelBindingResult.Success(re);
                }
                catch
                {

                }
            }
            return Task.CompletedTask;
        }
    }
}
