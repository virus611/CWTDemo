using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWTDemo.Lib;
using Microsoft.AspNetCore.Mvc;

namespace CWTDemo.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public Task<string> Index()
        {
            CWTUserModel obj = new CWTUserModel()
            {
                UID = 12345,
                Expired = DateTime.Now.AddDays(1)
            };
            string json = CWTUserModel.toToken(obj);
            return Task.FromResult(json);
        }
    }
}
