using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Domain.Entities
{
    public class COMPANY
    {
        public int COMPANY_ID { get; set; }
        public required string COMPANY_CODE { get; set; }
        public required string COMPANY_NAME { get; set; }
        public string? TAX_NO { get; set; }
        public string? PHONE { get; set; }
        public string? FAX { get; set; }
        public string? EMAIL { get; set; }
        public string? LINE { get; set; }
        public string? FACEBOOK { get; set; }
        public string? WEBSITE { get; set; }
        public string? LOGO { get; set; }
        public string? COMMENT { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }
        public int CREATED_BY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int LAST_UPDATE_ID { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public System.Guid ROW_UN { get; set; }

        public virtual ICollection<BRANCH> BRANCHES { get; set; }

        public COMPANY()
        {
            BRANCHES = new List<BRANCH>();
        }
    }
}
