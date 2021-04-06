using HT.Extensions;
using HTDal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HT.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class TestController : ControllerBase
    {

        private readonly CmDbContext dbCM;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        public TestController(ILogger<TestController> logger, CmDbContext cmDb)
        {
            dbCM = cmDb;
            _logger = logger;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>返回数据</returns>
        [HttpGet]
        [Authorize(Roles = "advanced")]
        public IActionResult GetData()
        {
            var name = User.FindFirst("Name")?.Value;
            var id = User.FindFirst("id")?.Value;

            var isTrialVersion = ConfigurationManager.AppSettings.IsTrialVersion;
            var data = dbCM.TaobaoShop.ToList();
            return Ok("123");
        }


    }
}
