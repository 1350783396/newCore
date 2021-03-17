using HTDal;
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
    public class TestController : Controller
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
        public IActionResult GetData()
        {
            var data = dbCM.TaobaoShop.ToList();
            return Ok("123");
        }


    }
}
