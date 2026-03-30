using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Domain.Entities
{
    public class PURCHASE_ORDER
    {
        public int PURCHASE_ORDER_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int BRANCH_ID { get; set; }
        public required string PURCHASE_ORDER_NO { get; set; }
        public DateTime PURCHASE_ORDER_DATE { get; set; }
        public int VENDOR_ID { get; set; }
        public string? VENDOR_CODE { get; set; }
        public string? VENDOR_FULLNAME { get; set; }
        public decimal VAT_RATE { get; set; }
        public decimal SUB_TOTAL { get; set; }
        public decimal SUB_TOTAL_FC { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal DISCOUNT_FC { get; set; }
        public decimal PRICE_AFTER_DISCOUNT { get; set; }
        public decimal PRICE_AFTER_DISCOUNT_FC { get; set; }
        public decimal PRICE_BEFORE_VAT { get; set; }
        public decimal PRICE_BEFORE_VAT_FC { get; set; }
        public decimal PRICE_BEFORE_VAT_BASE { get; set; }
        public decimal VAT { get; set; }
        public decimal PRICE_AFTER_VAT { get; set; }
        public decimal PRICE_AFTER_VAT_FC { get; set; }
        public decimal GRAND_TOTAL { get; set; }
        public decimal GRAND_TOTAL_FC { get; set; }
        public string? REMARK { get; set; }
        public int? DOCUMENT_STATUS_ID { get; set; }
        public string? DOCUMENT_STATUS_CODE { get; set; }
        public string? DOCUMENT_STATUS_NAME { get; set; }
        public string? DOCUMENT_STATUS_REASON { get; set; }
        public string? COMMENTS { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }
        public int CREATED_BY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int LAST_UPDATE_ID { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Guid ROW_UN { get; set; }

        public virtual COMPANY COMPANY { get; set; }
        public virtual BRANCH BRANCH { get; set; }
        public virtual VENDOR VENDOR { get; set; }
        public virtual FLEX_ITEM DOCUMENT_STATUS { get; set; }
    }
}
