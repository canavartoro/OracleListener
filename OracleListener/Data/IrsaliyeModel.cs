using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleListener.Data
{
    public class IrsaliyeModel
    {
        public IrsaliyeModel() { }

        public int ITEM_M_ID { get; set; }
        public string DOC_NO { get; set; }
        public DateTime DOC_DATE { get; set; }
        public int ENTITY_ID { get; set; }
        public string ENTITY_CODE { get; set; }
        public string ENTITY_NAME { get; set; }
        public int PURCHASE_SALES { get; set; }
        public string ALIS_SATIS { get; set; }
        public string CAT_CODE { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public int UNIT_ID { get; set; }
        public string UNIT_CODE { get; set; }
        public decimal QTY { get; set; }
        public decimal UNIT_PRICE { get; set; }
        public int WHOUSE_ID { get; set; }
    }
}

/*
SELECT M.ITEM_M_ID,M.DOC_NO,M.DOC_DATE,EN.ENTITY_ID,ENTITY_CODE,EN.ENTITY_NAME,M.PURCHASE_SALES,
CASE WHEN M.PURCHASE_SALES = 2 THEN 'SATIS' ELSE 'ALIS' END ALIS_SATIS,CT.CAT_CODE,
D.ITEM_ID,IT.ITEM_CODE,IT.ITEM_NAME,D.UNIT_ID,UN.UNIT_CODE,D.QTY,D.WHOUSE_ID,D.UNIT_PRICE
FROM UYUMSOFT.INVT_ITEM_M M INNER JOIN UYUMSOFT.FIND_ENTITY EN ON M.ENTITY_ID = EN.ENTITY_ID LEFT JOIN 
UYUMSOFT.GNLD_CATEGORY CT ON M.CAT_CODE1_ID = CT.CAT_CODE_ID INNER JOIN 
UYUMSOFT.INVT_ITEM_D D ON D.ITEM_M_ID = M.ITEM_M_ID INNER JOIN 
UYUMSOFT.INVD_ITEM IT ON D.ITEM_ID = IT.ITEM_ID INNER JOIN INVD_UNIT UN ON D.UNIT_ID = UN.UNIT_ID
WHERE ( 1 = 1 ) AND M.ITEM_M_ID = 39341
AND M.BRANCH_ID = '6373' AND M.CO_ID = '2379' AND M.INVOICE_STATUS = 3 AND CT.CAT_CODE = 'R' 
*/
