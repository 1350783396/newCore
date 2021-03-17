using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HTDal.Model
{
    [Table("TB_TaobaoShop")]
    public class TB_TaobaoShop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ShopID { get; set; }

        public string ShopName { get; set; }

        public string Url { get; set; }
        public decimal? fee { get; set; }
        public DateTime? CreateDate { get; set; }
        public decimal SWorkMoney { get; set; }

        public decimal SExpreeMoney { get; set; }

        public Int16? AutoPFlag { get; set; }

        public string AccessToken { get; set; }
        public DateTime? AccessEnd { get; set; }

        public DateTime? LastTradeModifyDate { get; set; }


        public DateTime? LastRefundDate { get; set; }

        public string UserID { get; set; }

        public int? get_order { get; set; }
    }
}
