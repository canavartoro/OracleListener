using OracleListener.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleListener.Data
{
    public class DataSynchronization : OracleProvider
    {
        public DataSynchronization()
        {
            sqlClient = new SqlClient(AppConfig.Default.GetSqlConnectionString());
        }

        private SqlClient sqlClient = null;

        public void StokSynchronization(string stok)
        {

        }

        public void DepoSynchronization(string depo = null)
        {
            string condition = string.Empty;
            if (!string.IsNullOrWhiteSpace(depo))
                condition = $" AND WH.WHOUSE_ID = '{depo}' ";
            var depolist = Select<DepoModel>($@"SELECT WH.WHOUSE_ID,WH.WHOUSE_CODE,WH.DESCRIPTION WHOUSE_DESC,WH.ISPASSIVE,WH.ISNEGATIVE,WH.ENTITY_ID,WH.CREATE_DATE,WH.UPDATE_DATE,
BW.TEL1,BW.EMAIL,BW.ADDRESS1,BW.ADDRESS2,BW.ADDRESS3,CT.CITY_NAME,TW.TOWN_NAME,CN.COUNTRY_NAME 
FROM INVD_WHOUSE WH INNER JOIN 
INVD_BRANCH_WHOUSE BW ON WH.WHOUSE_ID = BW.WHOUSE_ID LEFT JOIN 
GNLD_CITY CT ON BW.CITY_ID = CT.CITY_ID LEFT JOIN GNLD_TOWN TW ON BW.TOWN_ID = TW.TOWN_ID LEFT JOIN
GNLD_COUNTRY CN ON BW.COUNTRY_ID = CN.COUNTRY_ID
WHERE BW.BRANCH_ID = {AppConfig.Default.BranchId} {condition}
ORDER BY WH.WHOUSE_CODE");
            if (depolist != null && depolist.Count > 0)
            {
                for (int i = 0; i < depolist.Count; i++)
                {
                    var t = sqlClient.Count($@"SELECT COUNT(*) AS TX FROM dbo.F_DEPOT WITH (NOLOCK) WHERE DE_Code = N'{depolist[i].WHOUSE_CODE}'");
                    if (t == 0)
                    {
                        SqlParameter[] parameters = new SqlParameter[24];
                        parameters[0] = new SqlParameter("@P1", depolist[i].WHOUSE_ID);
                        parameters[1] = new SqlParameter("@P2", depolist[i].WHOUSE_DESC.GetParamValue());
                        parameters[2] = new SqlParameter("@P3", depolist[i].ADDRESS1.GetParamValue());
                        parameters[3] = new SqlParameter("@P4", depolist[i].COMPLEMENT.GetParamValue());
                        parameters[4] = new SqlParameter("@P5", depolist[i].POST_CODE.GetParamValue());
                        parameters[5] = new SqlParameter("@P6", depolist[i].CITY_NAME.GetParamValue());
                        parameters[6] = new SqlParameter("@P7", DBNull.Value); // DE_Contact     depo sorumlusu
                        parameters[7] = new SqlParameter("@P8", 1);  //DE_Principal
                        parameters[8] = new SqlParameter("@P9", 1);  //DE_CatCompta
                        parameters[9] = new SqlParameter("@P10", DBNull.Value); // DE_Region
                        parameters[10] = new SqlParameter("@P11", depolist[i].COUNTRY_NAME.GetParamValue());// DE_Pays
                        parameters[11] = new SqlParameter("@P12", depolist[i].EMAIL.GetParamValue());
                        parameters[12] = new SqlParameter("@P13", depolist[i].WHOUSE_CODE.GetParamValue());
                        parameters[13] = new SqlParameter("@P14", depolist[i].PHONE.GetParamValue());
                        parameters[14] = new SqlParameter("@P15", depolist[i].PHONE2.GetParamValue());
                        parameters[15] = new SqlParameter("@P16", 1);//   DE_Replication
                        parameters[16] = new SqlParameter("@P17", DBNull.Value); //   DP_NoDefaut
                        parameters[17] = new SqlParameter("@P18", 1);//   cbDP_NoDefaut
                        parameters[18] = new SqlParameter("@P19", 1);//   DE_Exclure
                        parameters[19] = new SqlParameter("@P20", 1);//   DE_Souche01
                        parameters[20] = new SqlParameter("@P21", 1);//   DE_Souche02
                        parameters[21] = new SqlParameter("@P22", 1);//   DE_Souche03
                        parameters[22] = new SqlParameter("@P23", AppConfig.Default.CreateUser.GetParamValue());//   cbCreateur
                        parameters[23] = new SqlParameter("P24", AppConfig.Default.CreateUserId.GetParamValue());//   cbCreationUser
                        var obj = sqlClient.ExecuteScalar($@"DECLARE @TableMarq TABLE (cbMarq int);
INSERT INTO F_DEPOT (DE_No,DE_Intitule,DE_Adresse,DE_Complement,DE_CodePostal,DE_Ville,DE_Contact,DE_Principal,DE_CatCompta,DE_Region,DE_Pays,DE_EMail,DE_Code,DE_Telephone,DE_Telecopie,DE_Replication,DP_NoDefaut,cbDP_NoDefaut,DE_Exclure,DE_Souche01,DE_Souche02,DE_Souche03,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24);
SELECT cbMarq FROM @TableMarq;", parameters);
                        if (obj != null)
                        {
                            Int16 dbtype = 0;
                            parameters = new SqlParameter[8];
                            parameters[0] = new SqlParameter("@P1", depolist[i].WHOUSE_ID);
                            parameters[1] = new SqlParameter("@P2", depolist[i].WHOUSE_ID);
                            parameters[2] = new SqlParameter("@P3", depolist[i].WHOUSE_CODE);
                            parameters[3] = new SqlParameter("@P4", depolist[i].WHOUSE_DESC.GetParamValue());
                            parameters[4] = new SqlParameter("@P5", DBNull.Value);//    DP_Zone
                            parameters[5] = new SqlParameter("@P6", dbtype);//  DP_Type
                            parameters[6] = new SqlParameter("@P7", AppConfig.Default.CreateUser.GetParamValue());//   cbCreateur
                            parameters[7] = new SqlParameter("@P8", AppConfig.Default.CreateUserId.GetParamValue());//   cbCreationUser
                            sqlClient.Execute($@"DECLARE @TableMarq TABLE (cbMarq int);
INSERT INTO F_DEPOTEMPL (DE_No,DP_No,DP_Code,DP_Intitule,DP_Zone,DP_Type,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8); SELECT cbMarq FROM @TableMarq;", parameters);
                        }
                    }
                }
            }

        }

        #region IDisposable
        ~DataSynchronization()
        {
            Dispose(false);
        }


        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                sqlClient?.Dispose();
            }

            sqlClient = null;
            disposed = true;
        }
        #endregion
    }
}
