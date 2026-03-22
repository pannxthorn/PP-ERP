using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.DTO.User
{
    public class PARAM_USER_DTO
    {
        public int USER_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? BRANCH_ID { get; set; }
        public required string USERNAME { get; set; }
        public string? PASSWORD { get; set; }
        public string? COMMENT { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }

        [SetsRequiredMembers]
        public PARAM_USER_DTO() { }
    }
}
