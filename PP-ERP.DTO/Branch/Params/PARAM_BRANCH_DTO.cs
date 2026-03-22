using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.DTO.Branch
{
    public class PARAM_BRANCH_DTO
    {
        public int BRANCH_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public required string BRANCH_CODE { get; set; }
        public required string BRANCH_NAME { get; set; }
        public string? PHONE { get; set; }
        public string? EMAIL { get; set; }
        public string? LINE { get; set; }
        public string? FACEBOOK { get; set; }
        public string? COMMENT { get; set; }
        public bool IS_HEADQUARTER { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }

        [SetsRequiredMembers]
        public PARAM_BRANCH_DTO() { }
    }
}
