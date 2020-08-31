using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWTDemo.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CWTDemo.Controllers
{
    /// <summary>
    /// 建议用postman等设置header["UID"]后调用
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public Task<string> Index(CWTUser model)
        {
            if (model == null)
            {
                return Task.FromResult("映射失败");
            }
            return Task.FromResult(model.UID.ToString());
        }
    }
}
