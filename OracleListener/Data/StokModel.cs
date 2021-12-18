using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleListener.Data
{
    [System.ComponentModel.Description("F_ARTICLE,F_ARTCLIENT")]
    public class StokModel
    {
        public StokModel() { }

        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public int UNIT_ID { get; set; }
        public string UNIT_CODE { get; set; }
        public string SALES_ACC_CODE { get; set; }
        public string PURCHASE_ACC_CODE { get; set; }
    }
}
