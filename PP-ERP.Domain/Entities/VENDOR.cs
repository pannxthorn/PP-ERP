using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Domain.Entities
{
    public class VENDOR
    {
        public int VENDOR_ID { get; set; }
        public string? VENDOR_CODE { get; set; }
        public string? VENDOR_NAME { get; set; }
        public string? VENDOR_NAME_ENG { get; set; }
        public string? VENDOR_FULLNAME { get; set; }
        public string? VENDOR_FULLNAME_ENG { get; set; }
        public int? TITLE_ID { get; set; }
        public string? COMMENTS { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }
        public int CREATED_BY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int LAST_UPDATE_ID { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Guid ROW_UN { get; set; }

        public virtual FLEX_ITEM TITLE { get; set; }
    }
}
