using HTDal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTDal
{
    public class CmDbContext : DbContext
    {
        public CmDbContext(DbContextOptions<CmDbContext> options) : base(options)
        {

        }

        public DbSet<TB_TaobaoShop> TaobaoShop { get; set; }

    }
}
