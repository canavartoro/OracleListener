using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleListener.Data
{
    [System.ComponentModel.Description("F_DEPOT,F_DEPOTEMPL")]
    public class DepoModel
    {
        public DepoModel() { }

        [System.ComponentModel.Description("DE_No")]
        public int WHOUSE_ID { get; set; }

        [System.ComponentModel.Description("DE_Code")]
        public string WHOUSE_CODE { get; set; }

        [System.ComponentModel.Description("DE_Intitule")]
        public string WHOUSE_DESC { get; set; }

        public bool ISPASSIVE { get; set; }

        public bool ISNEGATIVE { get; set; }

        public int ENTITY_ID { get; set; }

        public DateTime CREATE_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public bool SELECTED { get; set; }

        [System.ComponentModel.Description("DE_Adresse")]
        public string ADDRESS1 { get; set; }

        [System.ComponentModel.Description("DE_Adresse")]
        public string ADDRESS2 { get; set; }

        [System.ComponentModel.Description("DE_Adresse")]
        public string ADDRESS3 { get; set; }

        [System.ComponentModel.Description("DE_Complement")]
        public string COMPLEMENT { get; set; }

        [System.ComponentModel.Description("DE_CodePostal")]
        public string POST_CODE { get; set; }

        [System.ComponentModel.Description("DE_Ville")]
        public string CITY_NAME { get; set; }

        [System.ComponentModel.Description("DE_Ville")]
        public string TOWN_NAME { get; set; }

        [System.ComponentModel.Description("DE_Contact")]
        public string CONTACT { get; set; }

        [System.ComponentModel.Description("DE_Principal")]
        public int PRINCIPLE { get; set; }

        [System.ComponentModel.Description("DE_Pays")]
        public string COUNTRY_NAME { get; set; }

        [System.ComponentModel.Description("DE_EMail")]
        public string EMAIL { get; set; }

        [System.ComponentModel.Description("DE_Telephone")]
        public string PHONE { get; set; }

        [System.ComponentModel.Description("DE_Telecopie")]
        public string PHONE2 { get; set; }
        
    }
}
