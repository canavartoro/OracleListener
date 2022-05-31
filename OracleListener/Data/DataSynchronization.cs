using OracleListener.Log;
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

        public void StokSynchronization(string stok = null)
        {
            int[] whouseIds = new int[0];
            string depotlist = FileHelper.ReadFile("depots.xml");
            if (!string.IsNullOrEmpty(depotlist))
            {
                var depots = FileHelper.FromXml(depotlist, typeof(List<DepoModel>)) as List<DepoModel>;
                whouseIds = (from q in depots where q.SELECTED select q.WHOUSE_ID).ToArray();
            }

            string condition = string.Empty;
            if (!string.IsNullOrWhiteSpace(stok))
                condition = $" AND IT.ITEM_ID = '{stok}' ";

            var itemlist = Select<StokModel>($@"SELECT IT.ITEM_ID,IT.ITEM_CODE,IT.ITEM_NAME,IT.UNIT_ID,UN.UNIT_CODE,
CASE WHEN SALES_PLN.HESAPPLANI_CODE IS NULL THEN TO_CHAR(SALES_ACC.ACC_CODE) ELSE TO_CHAR(SALES_PLN.HESAPPLANI_CODE) END SALES_ACC_CODE,
CASE WHEN PURCHASE_PLN.HESAPPLANI_CODE IS NULL THEN TO_CHAR(PURCHASE_ACC.ACC_CODE) ELSE TO_CHAR(PURCHASE_PLN.HESAPPLANI_CODE) END PURCHASE_ACC_CODE
FROM UYUMSOFT.INVD_ITEM IT INNER JOIN UYUMSOFT.INVD_BWH_ITEM BW ON IT.ITEM_ID = BW.ITEM_ID INNER JOIN 
UYUMSOFT.INVD_UNIT UN ON IT.UNIT_ID = UN.UNIT_ID INNER JOIN INVD_BRANCH_ITEM BT ON BT.ITEM_ID = IT.ITEM_ID AND BT.BRANCH_ID = BW.BRANCH_ID INNER JOIN 
INVD_ITEM_ACC_INTG INTG ON INTG.I_ACC_INTG_TYPE_CODE_ID = BT.I_ACC_INTG_TYPE_CODE_ID AND INTG.BRANCH_ID = BW.BRANCH_ID AND INTG.CO_ID = BW.CO_ID INNER JOIN
FIND_ACC PURCHASE_ACC ON INTG.PURCHASE_ACC_ID = PURCHASE_ACC.ACC_ID INNER JOIN FIND_ACC SALES_ACC ON INTG.SALES_ACC_ID = SALES_ACC.ACC_ID LEFT JOIN
ZFIND_SAGE_HESAPPLANI SALES_PLN ON SALES_ACC.ACC_CODE = SALES_PLN.ACC_CODE LEFT JOIN
ZFIND_SAGE_HESAPPLANI PURCHASE_PLN ON PURCHASE_ACC.ACC_CODE = PURCHASE_PLN.ACC_CODE
WHERE BW.BRANCH_ID = '{AppConfig.Default.BranchId}' AND BW.CO_ID = '{AppConfig.Default.CoId}' 
AND BW.WHOUSE_ID IN ({string.Join(",", whouseIds)}) {condition}
ORDER BY IT.ITEM_CODE");

            if (itemlist != null && itemlist.Count > 0)
            {
                for (int i = 0; i < itemlist.Count; i++)
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@ITEM_ID", itemlist[i].ITEM_ID);
                    parameters[1] = new SqlParameter("@ITEM_CODE", itemlist[i].ITEM_CODE.GetParamValue());
                    parameters[2] = new SqlParameter("@ITEM_NAME", itemlist[i].ITEM_NAME.GetParamValue());
                    parameters[3] = new SqlParameter("@UNIT_ID", itemlist[i].UNIT_ID);
                    parameters[4] = new SqlParameter("@UNIT_CODE", itemlist[i].UNIT_CODE.GetParamValue());
                    parameters[5] = new SqlParameter("@ACP_ComptaCPT_CompteG", itemlist[i].PURCHASE_ACC_CODE.GetParamValue());
                    parameters[6] = new SqlParameter("@ACP_ComptaCPT_CompteA", itemlist[i].SALES_ACC_CODE.GetParamValue());

                    sqlClient.Execute("EXECUTE dbo.ZZ_SP_CREATE_ARTICLE @ITEM_ID, @ITEM_CODE, @ITEM_NAME, @UNIT_ID, @UNIT_CODE, @ACP_ComptaCPT_CompteG, @ACP_ComptaCPT_CompteA", parameters);
                }
            }

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
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@WHOUSE_ID", depolist[i].WHOUSE_ID);
                    parameters[1] = new SqlParameter("@WHOUSE_CODE", depolist[i].WHOUSE_CODE.GetParamValue());
                    parameters[2] = new SqlParameter("@DESCRIPTION", depolist[i].WHOUSE_DESC.GetParamValue());

                    sqlClient.Execute("EXECUTE dbo.ZZ_SP_CREATE_DEPOT @WHOUSE_ID, @WHOUSE_CODE, @DESCRIPTION", parameters);

                    /*var t = sqlClient.Count($@"SELECT COUNT(*) AS TX FROM dbo.F_DEPOT WITH (NOLOCK) WHERE DE_Code = N'{depolist[i].WHOUSE_CODE}'");
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
                    }*/
                }
            }

        }

        public void SatisIrsaliyeSynchronization(string irsaliye = null)
        {
            string condition = string.Empty;
            if (!string.IsNullOrWhiteSpace(irsaliye))
                condition = $" AND M.ITEM_M_ID =  '{irsaliye}' ";

            var irslist = Select<IrsaliyeModel>($@"
SELECT M.ITEM_M_ID,M.DOC_NO,M.DOC_DATE,EN.ENTITY_ID,ENTITY_CODE,EN.ENTITY_NAME,M.PURCHASE_SALES,
CASE WHEN M.PURCHASE_SALES = 2 THEN 'SATIS' ELSE 'ALIS' END ALIS_SATIS,CT.CAT_CODE,
D.ITEM_ID,IT.ITEM_CODE,IT.ITEM_NAME,D.UNIT_ID,UN.UNIT_CODE,D.QTY,D.WHOUSE_ID,D.UNIT_PRICE,
SALES_PLN.HESAPPLANI_CODE SALES_ACC_CODE
FROM UYUMSOFT.INVT_ITEM_M M INNER JOIN UYUMSOFT.FIND_ENTITY EN ON M.ENTITY_ID = EN.ENTITY_ID LEFT JOIN 
UYUMSOFT.GNLD_CATEGORY CT ON M.CAT_CODE1_ID = CT.CAT_CODE_ID INNER JOIN 
UYUMSOFT.INVT_ITEM_D D ON D.ITEM_M_ID = M.ITEM_M_ID INNER JOIN 
UYUMSOFT.INVD_ITEM IT ON D.ITEM_ID = IT.ITEM_ID INNER JOIN INVD_UNIT UN ON D.UNIT_ID = UN.UNIT_ID LEFT JOIN
UYUMSOFT.INVD_BRANCH_ITEM BT ON BT.ITEM_ID = IT.ITEM_ID AND BT.BRANCH_ID = D.BRANCH_ID LEFT JOIN
INVD_ITEM_ACC_INTG INTG ON INTG.I_ACC_INTG_TYPE_CODE_ID = BT.I_ACC_INTG_TYPE_CODE_ID AND INTG.BRANCH_ID = BT.BRANCH_ID AND INTG.CO_ID = BT.CO_ID LEFT JOIN
FIND_ACC SALES_ACC ON INTG.SALES_ACC_ID = SALES_ACC.ACC_ID LEFT JOIN ZFIND_SAGE_HESAPPLANI SALES_PLN ON SALES_ACC.ACC_CODE = SALES_PLN.ACC_CODE
WHERE ( 1 = 1 ) {condition} AND M.ZZ_DOCENTETE_ID IS NULL AND CAST(TO_CHAR(M.DOC_DATE,'YY') AS NUMBER) = {DateTime.Now:yy}
AND M.BRANCH_ID = '{AppConfig.Default.BranchId}' AND M.CO_ID = '{AppConfig.Default.CoId}' AND M.INVOICE_STATUS = 3 AND CT.CAT_CODE = 'R'");

            if (irslist != null && irslist.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@DO_Piece", irslist[0].DOC_NO.GetParamValue()),
                    new SqlParameter("@DO_Date", irslist[0].DOC_DATE.Date),
                    new SqlParameter("@ENTITY_ID", irslist[0].ENTITY_ID),
                    new SqlParameter("@DO_Tiers", irslist[0].ENTITY_CODE.GetParamValue()),
                    new SqlParameter("@CT_Intitule", irslist[0].ENTITY_NAME.GetParamValue()),
                    new SqlParameter("@DE_No", irslist[0].WHOUSE_ID),
                    new SqlParameter("@AR_Ref", irslist[0].ITEM_CODE.GetParamValue()),
                    new SqlParameter("@DL_Design", irslist[0].ITEM_NAME.GetParamValue()),
                    new SqlParameter("@DL_Qte", irslist[0].QTY),
                    new SqlParameter("@UnitPrice", irslist[0].UNIT_PRICE),
                    new SqlParameter("@ACP_Satis", irslist[0].SALES_ACC_CODE.GetParamValue())
            };

                var cbMarq = sqlClient.ExecuteScalar("EXECUTE dbo.ZZ_SP_CREATE_IRSALIYE_SATIS @DO_Piece,@DO_Date,@ENTITY_ID,@DO_Tiers,@CT_Intitule,@DE_No,@AR_Ref,@DL_Design,@DL_Qte,@UnitPrice,@ACP_Satis", parameters);
                if (cbMarq != null)
                {
                    if (!Execute($@"UPDATE ""UYUMSOFT"".""INVT_ITEM_M"" SET ""ZZ_DOCENTETE_ID"" = {cbMarq} WHERE ITEM_M_ID = {irslist[0].ITEM_M_ID}"))
                    {
                        Logger.E($"İrsaliye bilgisi güncellenemedi! Id:{cbMarq}, Hata: {Message}, Sql:{SqlString}");
                    }
                }

            }

        }

        public void AlisIrsaliyeSynchronization(string irsaliye = null)
        {
            try
            {
                sqlClient.Begin();

                string condition = string.Empty;
                if (!string.IsNullOrWhiteSpace(irsaliye))
                    condition = $" AND M.ITEM_M_ID =  '{irsaliye}' ";

                var irslist = Select<IrsaliyeModel>($@"
SELECT M.ITEM_M_ID,M.DOC_NO,M.DOC_DATE,EN.ENTITY_ID,ENTITY_CODE,EN.ENTITY_NAME,M.PURCHASE_SALES,
CASE WHEN M.PURCHASE_SALES = 2 THEN 'SATIS' ELSE 'ALIS' END ALIS_SATIS,CT.CAT_CODE
FROM UYUMSOFT.INVT_ITEM_M M INNER JOIN UYUMSOFT.FIND_ENTITY EN ON M.ENTITY_ID = EN.ENTITY_ID LEFT JOIN 
UYUMSOFT.GNLD_CATEGORY CT ON M.CAT_CODE1_ID = CT.CAT_CODE_ID 
WHERE ( 1 = 1 ) {condition} AND M.ZZ_DOCENTETE_ID IS NULL AND CAST(TO_CHAR(M.DOC_DATE,'YY') AS NUMBER) = {DateTime.Now:yy}
AND M.BRANCH_ID = '{AppConfig.Default.BranchId}' AND M.CO_ID = '{AppConfig.Default.CoId}' AND M.INVOICE_STATUS = 3 AND CT.CAT_CODE = 'R'");
                if (irslist != null && irslist.Count > 0)
                {
                    SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@DO_Piece", irslist[0].DOC_NO.GetParamValue()),
                    new SqlParameter("@DO_Date", irslist[0].DOC_DATE.Date),
                    new SqlParameter("@ENTITY_ID", irslist[0].ENTITY_ID),
                    new SqlParameter("@DO_Tiers", irslist[0].ENTITY_CODE.GetParamValue()),
                    new SqlParameter("@CT_Intitule", irslist[0].ENTITY_NAME.GetParamValue())
                };

                    var cbMarq = sqlClient.ExecuteScalar("EXECUTE dbo.ZZ_SP_CREATE_IRSALIYE_ALIS @DO_Piece,@DO_Date,@ENTITY_ID,@DO_Tiers,@CT_Intitule", parameters);
                    if (cbMarq != null)
                    {
                        var detlist = Select<IrsaliyeModel>($@"
SELECT D.ITEM_ID,IT.ITEM_CODE,IT.ITEM_NAME,D.UNIT_ID,UN.UNIT_CODE,D.QTY,D.WHOUSE_ID,D.UNIT_PRICE,
CASE WHEN PURCHASE_PLN.HESAPPLANI_CODE IS NULL THEN TO_CHAR(PURCHASE_ACC.ACC_CODE) ELSE TO_CHAR(PURCHASE_PLN.HESAPPLANI_CODE) END PURCHASE_ACC_CODE
FROM UYUMSOFT.INVT_ITEM_D D LEFT JOIN 
UYUMSOFT.INVD_ITEM IT ON D.ITEM_ID = IT.ITEM_ID INNER JOIN 
INVD_UNIT UN ON D.UNIT_ID = UN.UNIT_ID INNER JOIN INVD_BRANCH_ITEM BT ON BT.ITEM_ID = IT.ITEM_ID AND BT.BRANCH_ID = D.BRANCH_ID INNER JOIN 
INVD_ITEM_ACC_INTG INTG ON INTG.I_ACC_INTG_TYPE_CODE_ID = BT.I_ACC_INTG_TYPE_CODE_ID AND INTG.BRANCH_ID = BT.BRANCH_ID AND INTG.CO_ID = BT.CO_ID INNER JOIN
FIND_ACC PURCHASE_ACC ON INTG.PURCHASE_ACC_ID = PURCHASE_ACC.ACC_ID LEFT JOIN
ZFIND_SAGE_HESAPPLANI PURCHASE_PLN ON PURCHASE_ACC.ACC_CODE = PURCHASE_PLN.ACC_CODE
WHERE D.ITEM_M_ID = {irslist[0].ITEM_M_ID}");

                        if (detlist != null && detlist.Count > 0)
                        {
                            for (int i = 0; i < detlist.Count; i++)
                            {
                                SqlParameter[] parametersd = new SqlParameter[] {
                                new SqlParameter("@DO_Piece", irslist[0].DOC_NO.GetParamValue()),
                                new SqlParameter("@DO_Date", irslist[0].DOC_DATE.Date),
                                new SqlParameter("@CT_Num", irslist[0].ENTITY_CODE.GetParamValue()),
                                new SqlParameter("@DE_No", detlist[i].WHOUSE_ID),
                                new SqlParameter("@AR_Ref", detlist[i].ITEM_CODE.GetParamValue()),
                                new SqlParameter("@DL_Design", detlist[i].ITEM_NAME.GetParamValue()),
                                new SqlParameter("@DL_Qte", detlist[i].QTY),
                                new SqlParameter("@UnitPrice", detlist[i].UNIT_PRICE),
                                new SqlParameter("@ACP_Alis", detlist[i].PURCHASE_ACC_CODE.GetParamValue())
                            };
                                var cbMarqd = sqlClient.ExecuteScalar("EXECUTE dbo.ZZ_SP_CREATE_IRSALIYE_ALIS_DETAY @DO_Piece,@DO_Date,@CT_Num,@DE_No,@AR_Ref,@DL_Design,@DL_Qte,@UnitPrice,@ACP_Alis", parametersd);
                                if (cbMarqd == null)
                                {
                                    sqlClient.Rollback();
                                    Log.Logger.E($"İrsaliye oluşturulamadı! Id:{cbMarq}, Hata: {Message}, Sql:{SqlString}");
                                }
                            }
                        }

                        if (!Execute($@"UPDATE ""UYUMSOFT"".""INVT_ITEM_M"" SET ""ZZ_DOCENTETE_ID"" = {cbMarq} WHERE ITEM_M_ID = {irslist[0].ITEM_M_ID}"))
                        {
                            Log.Logger.E($"İrsaliye bilgisi güncellenemedi! Id:{cbMarq}, Hata: {Message}, Sql:{SqlString}");
                        }
                    }

                }

                sqlClient.Commit();
            }
            catch (Exception exc)
            {
                Log.Logger.E(exc);
            }
            finally
            {
                sqlClient.Rollback();
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

