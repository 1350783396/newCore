using AutoMapper;
using HT.Extensions;
using HT.Utils;
using HT.ViewModels;
using HTDal;
using HTDal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HT.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class TestController : ControllerBase
    {

        private readonly CmDbContext dbCM;
        private readonly ILogger _logger;
        private readonly IMapper autoMapper;
        private readonly IDatabase redis;
        /// <summary>
        /// 
        /// </summary>
        public TestController(ILogger<TestController> logger, CmDbContext cmDb, IMapper mapper, RedisHelper redisHelper)
        {
            dbCM = cmDb;
            _logger = logger;
            autoMapper = mapper;
            redis = redisHelper.GetDatabase();
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
        [HttpGet]
        public IActionResult TestAutoMapper()
        {
            TB_TaobaoShop taobao = new TB_TaobaoShop() { id = 1, ShopName = "天猫" };
            TaobaoShopView tb = autoMapper.Map<TB_TaobaoShop, TaobaoShopView>(taobao);

            return Ok("123");
        }
        [HttpGet]
        public IActionResult TestRedis()
        {
            redis.StringSet("xingming", "111");
            string xingming = redis.StringGet("xingming");
            return Ok(xingming);
        }
    }
}
