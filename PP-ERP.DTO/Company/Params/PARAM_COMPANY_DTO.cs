using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.DTO.Company
{
    public class PARAM_COMPANY_DTO
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

        [SetsRequiredMembers]
        public PARAM_COMPANY_DTO() { }
    }
}
