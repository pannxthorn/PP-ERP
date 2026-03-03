using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Domain.Entities
{
    public class FLEX
    {
        public int FLEX_ID { get; set; }
        public required string FLEX_CODE { get; set; }
        public required string FLEX_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }
        public int CREATED_BY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int LAST_UPDATE_ID { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public System.Guid ROW_UN { get; set; }

        public virtual ICollection<FLEX_ITEM> FLEX_ITEMS { get; set; }
        public FLEX()
        {
            FLEX_ITEMS = new List<FLEX_ITEM>();
        }
    }
}
