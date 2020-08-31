using CWTDemo.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CWTDemo.Lib
{
    /// <summary>
    /// 登录信息类
    /// 可以自己加入一些不是很重要的属性进来，比如登录后的用户名
    /// </summary>
    public class CWTUserModel
    {
        public long UID { get; set; }
        public DateTime Expired { get; set; }



        public static string toToken(CWTUserModel obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return AESTools.AESEncrypt(json);
        }
    }


    /// <summary>
    /// 传给controller的类
    /// 本例只用了UID
    /// </summary>
    [ModelBinder(BinderType = typeof(CWTUserBinder))]
    public class CWTUser
    {
        public long UID { get; set; }

    }
}
