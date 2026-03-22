using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.DTO.User
{
    public class RESULT_USER_DTO
    {
        public int USER_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? BRANCH_ID { get; set; }
        public required string USERNAME { get; set; }
        public string? COMMENT { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }
        public int CREATED_BY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int LAST_UPDATE_ID { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public System.Guid ROW_UN { get; set; }
    }
}
