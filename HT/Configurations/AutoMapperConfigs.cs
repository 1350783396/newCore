using AutoMapper;
using HT.ViewModels;
using HTDal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HT.Configurations
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<TB_TaobaoShop, TaobaoShopView>();

        }


    }
}
