
ALTER PROCEDURE [dbo].[ZZ_SP_CREATE_ARTICLE](
	@ITEM_ID INTEGER,
	@ITEM_CODE NVARCHAR(60),
	@ITEM_NAME NVARCHAR(150),
	@UNIT_ID INTEGER,
	@UNIT_CODE NVARCHAR(10),
	@ACP_ComptaCPT_CompteG VARCHAR(13),
	@ACP_ComptaCPT_CompteA VARCHAR(13) 
)
AS 
--EXECUTE dbo.ZZ_SP_CREATE_ARTICLE 120933, '04 017', 'BETON KATKISI-CHRYSO DELTA 4540', 164, 'KG', '17800000', '17800000'
BEGIN
DECLARE 
@AR_Ref varchar(19) = @ITEM_CODE,
@AR_Design varchar(69) = @ITEM_NAME,
@FA_CodeFamille varchar(11) = N'ENT',
@AR_Substitut varchar(19) = NULL,
@AR_Raccourci varchar(7) = NULL,
@AR_Garantie smallint = 0,
@AR_UnitePoids smallint = 3,
@AR_PoidsNet numeric(24, 6) = 0,
@AR_PoidsBrut numeric(24, 6) = 0,
@AR_UniteVen smallint = 1,
@AR_PrixAch numeric(24, 6) = 0,
@AR_Coef numeric(24, 6) = 0,
@AR_PrixVen numeric(24, 6) = 0,
@AR_PrixTTC smallint = 0,
@AR_Gamme1 smallint = 0,
@AR_Gamme2 smallint = 0,
@AR_SuiviStock smallint = 2,
@AR_Nomencl smallint = 0,
@AR_Stat01 varchar(21) = '',
@AR_Stat02 varchar(21) = '',
@AR_Stat03 varchar(21) = '',
@AR_Stat04 varchar(21) = '',
@AR_Stat05 varchar(21) = '',
@AR_Escompte smallint = 0,
@AR_Delai smallint = 0,
@AR_HorsStat smallint = 0,
@AR_VteDebit smallint = 0,
@AR_NotImp smallint = 0,
@AR_Sommeil smallint = 0,
@AR_Langue1 varchar(69) = '',
@AR_Langue2 varchar(69) = '',
@AR_EdiCode varchar(45) = 22,
@AR_CodeBarre varchar(19) = '',
@AR_CodeFiscal varchar(25) = '',
@AR_Pays varchar(35) = '',
@AR_Frais01FR_Denomination varchar(21) = N'Coût de stockage',
@AR_Frais01FR_Rem01REM_Valeur numeric(24, 6) = 0,
@AR_Frais01FR_Rem01REM_Type smallint = 0,
@AR_Frais01FR_Rem02REM_Valeur numeric(24, 6) = 0,
@AR_Frais01FR_Rem02REM_Type smallint = 0,
@AR_Frais01FR_Rem03REM_Valeur numeric(24, 6) = 0,
@AR_Frais01FR_Rem03REM_Type smallint = 0,
@AR_Frais02FR_Denomination varchar(21) = 0,
@AR_Frais02FR_Rem01REM_Valeur numeric(24, 6) = 0,
@AR_Frais02FR_Rem01REM_Type smallint = 0,
@AR_Frais02FR_Rem02REM_Valeur numeric(24, 6) = 0,
@AR_Frais02FR_Rem02REM_Type smallint = 0,
@AR_Frais02FR_Rem03REM_Valeur numeric(24, 6) = 0,
@AR_Frais02FR_Rem03REM_Type smallint = 0,
@AR_Frais03FR_Denomination varchar(21) = N'Coût annexe',
@AR_Frais03FR_Rem01REM_Valeur numeric(24, 6) = 0,
@AR_Frais03FR_Rem01REM_Type smallint = 0,
@AR_Frais03FR_Rem02REM_Valeur numeric(24, 6) = 0,
@AR_Frais03FR_Rem02REM_Type smallint = 0,
@AR_Frais03FR_Rem03REM_Valeur numeric(24, 6) = 0,
@AR_Frais03FR_Rem03REM_Type smallint = 0,
@AR_Condition smallint = 0,
@AR_PUNet numeric(24, 6) = 0,
@AR_Contremarque smallint = 0,
@AR_FactPoids smallint = 0,
@AR_FactForfait smallint = 0,
@AR_SaisieVar smallint = 0,
@AR_Transfere smallint = 0,
@AR_Publie smallint = 1,
@AR_DateModif datetime = GETDATE(),
@AR_Photo varchar(259) = NULL,
@AR_PrixAchNouv numeric(24, 6) = 0,
@AR_CoefNouv numeric(24, 6) = 0,
@AR_PrixVenNouv numeric(24, 6) = 0,
@AR_DateApplication datetime = GETDATE(),
@AR_CoutStd numeric(24, 6) = 0,
@AR_QteComp numeric(24, 6) = 1,
@AR_QteOperatoire numeric(24, 6) = 1,
@CO_No int = 0,
@cbCO_No int = NULL,
@AR_Prevision smallint = 0,
@CL_No1 int = NULL,
@cbCL_No1 int = NULL,
@CL_No2 int = NULL,
@cbCL_No2 int = NULL,
@CL_No3 int = NULL,
@cbCL_No3 int = NULL,
@CL_No4 int = NULL,
@cbCL_No4 int = NULL,
@AR_Type smallint = 0,
@RP_CodeDefaut varchar(11) = NULL,
@AR_Nature smallint = 2,
@AR_DelaiFabrication smallint = 0,
@AR_NbColis smallint = 0,
@AR_DelaiPeremption smallint = 0,
@AR_DelaiSecurite smallint = 0,
@AR_Fictif smallint = 0,
@AR_SousTraitance smallint = 0,
@AR_TypeLancement smallint = 0,
@AR_Cycle smallint = 0,
@AR_Criticite smallint = 0,
@cbProt smallint = 0,
@cbMarq int = NULL,
@cbCreateur char(4) = 'COLU',
@cbModification datetime = NULL,
@cbReplication int = 0,
@cbFlag smallint = 0,
@cbCreation datetime = GETDATE(),
@cbCreationUser uniqueidentifier = '77384016-921F-472F-B56D-1D563B7DDF3C',
@AR_InterdireCommande smallint = 0,
@AR_Exclure smallint = 0,
@ACP_Type smallint = 1,
@ACP_Champ smallint = 1,
--@ACP_ComptaCPT_CompteG varchar(13) = '60110000',
--@ACP_ComptaCPT_CompteA varchar(13) = NULL,
@ACP_ComptaCPT_Taxe1 varchar(5) = NULL,
@ACP_ComptaCPT_Taxe2 varchar(5) = NULL,
@ACP_ComptaCPT_Taxe3 varchar(5) = NULL,
@ACP_ComptaCPT_Date1 datetime = '1753-01-01 00:00:00',
@ACP_ComptaCPT_Date2 datetime = '1753-01-01 00:00:00',
@ACP_ComptaCPT_Date3 datetime = '1753-01-01 00:00:00',
@ACP_ComptaCPT_TaxeAnc1 varchar(5) = NULL,
@ACP_ComptaCPT_TaxeAnc2 varchar(5) = NULL,
@ACP_ComptaCPT_TaxeAnc3 varchar(5) = NULL,
@ACP_TypeFacture smallint = 0

SELECT @AR_Ref = REPLACE(@ITEM_CODE,' ','')

IF EXISTS (SELECT * FROM dbo.F_ARTICLE WITH (NOLOCK) WHERE AR_Ref = @AR_Ref) BEGIN
	UPDATE dbo.F_ARTICLE SET AR_Design = @ITEM_NAME WHERE AR_Ref = @AR_Ref
END ELSE BEGIN
	DECLARE @ID INTEGER
	DECLARE @TableMarq TABLE (cbMarq int);
	
	INSERT INTO dbo.F_ARTICLE (AR_Ref,AR_Design,FA_CodeFamille,AR_Substitut,AR_Raccourci,AR_Garantie,AR_UnitePoids,AR_PoidsNet,AR_PoidsBrut,AR_UniteVen,AR_PrixAch,AR_Coef,AR_PrixVen,AR_PrixTTC,AR_Gamme1,AR_Gamme2,AR_SuiviStock,AR_Nomencl,AR_Stat01,AR_Stat02,AR_Stat03,AR_Stat04,AR_Stat05,AR_Escompte,AR_Delai,AR_HorsStat,AR_VteDebit,AR_NotImp,AR_Sommeil,AR_Langue1,AR_Langue2,AR_EdiCode,AR_CodeBarre,AR_CodeFiscal,AR_Pays,AR_Frais01FR_Denomination,AR_Frais01FR_Rem01REM_Valeur,AR_Frais01FR_Rem01REM_Type,AR_Frais01FR_Rem02REM_Valeur,AR_Frais01FR_Rem02REM_Type,AR_Frais01FR_Rem03REM_Valeur,AR_Frais01FR_Rem03REM_Type,AR_Frais02FR_Denomination,AR_Frais02FR_Rem01REM_Valeur,AR_Frais02FR_Rem01REM_Type,AR_Frais02FR_Rem02REM_Valeur,AR_Frais02FR_Rem02REM_Type,AR_Frais02FR_Rem03REM_Valeur,AR_Frais02FR_Rem03REM_Type,AR_Frais03FR_Denomination,AR_Frais03FR_Rem01REM_Valeur,AR_Frais03FR_Rem01REM_Type,AR_Frais03FR_Rem02REM_Valeur,AR_Frais03FR_Rem02REM_Type,AR_Frais03FR_Rem03REM_Valeur,AR_Frais03FR_Rem03REM_Type,AR_Condition,AR_PUNet,AR_Contremarque,AR_FactPoids,AR_FactForfait,AR_SaisieVar,AR_Transfere,AR_Publie,AR_DateModif,AR_Photo,AR_PrixAchNouv,AR_CoefNouv,AR_PrixVenNouv,AR_DateApplication,AR_CoutStd,AR_QteComp,AR_QteOperatoire,CO_No,cbCO_No,AR_Prevision,CL_No1,cbCL_No1,CL_No2,cbCL_No2,CL_No3,cbCL_No3,CL_No4,cbCL_No4,AR_Type,RP_CodeDefaut,AR_Nature,AR_DelaiFabrication,AR_NbColis,AR_DelaiPeremption,AR_DelaiSecurite,AR_Fictif,AR_SousTraitance,AR_TypeLancement,AR_Cycle,AR_Criticite,AR_InterdireCommande,AR_Exclure,cbCreateur,cbCreationUser)
	OUTPUT inserted.cbMarq INTO @TableMarq VALUES (@AR_Ref,@AR_Design,@FA_CodeFamille,@AR_Substitut,@AR_Raccourci,@AR_Garantie,@AR_UnitePoids,@AR_PoidsNet,@AR_PoidsBrut,@AR_UniteVen,@AR_PrixAch,@AR_Coef,@AR_PrixVen,@AR_PrixTTC,@AR_Gamme1,@AR_Gamme2,@AR_SuiviStock,@AR_Nomencl,@AR_Stat01,@AR_Stat02,@AR_Stat03,@AR_Stat04,@AR_Stat05,@AR_Escompte,@AR_Delai,@AR_HorsStat,@AR_VteDebit,@AR_NotImp,@AR_Sommeil,@AR_Langue1,@AR_Langue2,@AR_EdiCode,@AR_CodeBarre,@AR_CodeFiscal,@AR_Pays,@AR_Frais01FR_Denomination,@AR_Frais01FR_Rem01REM_Valeur,@AR_Frais01FR_Rem01REM_Type,@AR_Frais01FR_Rem02REM_Valeur,@AR_Frais01FR_Rem02REM_Type,@AR_Frais01FR_Rem03REM_Valeur,@AR_Frais01FR_Rem03REM_Type,@AR_Frais02FR_Denomination,@AR_Frais02FR_Rem01REM_Valeur,@AR_Frais02FR_Rem01REM_Type,@AR_Frais02FR_Rem02REM_Valeur,@AR_Frais02FR_Rem02REM_Type,@AR_Frais02FR_Rem03REM_Valeur,@AR_Frais02FR_Rem03REM_Type,@AR_Frais03FR_Denomination,@AR_Frais03FR_Rem01REM_Valeur,@AR_Frais03FR_Rem01REM_Type,@AR_Frais03FR_Rem02REM_Valeur,@AR_Frais03FR_Rem02REM_Type,@AR_Frais03FR_Rem03REM_Valeur,@AR_Frais03FR_Rem03REM_Type,@AR_Condition,@AR_PUNet,@AR_Contremarque,@AR_FactPoids,@AR_FactForfait,@AR_SaisieVar,@AR_Transfere,@AR_Publie,@AR_DateModif,@AR_Photo,@AR_PrixAchNouv,@AR_CoefNouv,@AR_PrixVenNouv,@AR_DateApplication,@AR_CoutStd,@AR_QteComp,@AR_QteOperatoire,@CO_No,@cbCO_No,@AR_Prevision,@CL_No1,@cbCL_No1,@CL_No2,@cbCL_No2,@CL_No3,@cbCL_No3,@CL_No4,@cbCL_No4,@AR_Type,@RP_CodeDefaut,@AR_Nature,@AR_DelaiFabrication,@AR_NbColis,@AR_DelaiPeremption,@AR_DelaiSecurite,@AR_Fictif,@AR_SousTraitance,@AR_TypeLancement,@AR_Cycle,@AR_Criticite,@AR_InterdireCommande,@AR_Exclure,@cbCreateur,@cbCreationUser)
	SELECT @cbMarq = cbMarq FROM @TableMarq;
	
	--SELECT * FROM dbo.F_CATALOGUE WITH (NOLOCK) WHERE CL_No = 1

	INSERT INTO F_ARTCLIENT (AR_Ref,AC_Categorie,AC_PrixVen,AC_Coef,AC_PrixTTC,AC_Arrondi,AC_QteMont,EG_Champ,AC_PrixDev,AC_Devise,CT_Num,AC_Remise,AC_Calcul,AC_TypeRem,AC_RefClient,AC_CoefNouv,AC_PrixVenNouv,AC_PrixDevNouv,AC_RemiseNouv,AC_DateApplication,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq 
	VALUES (@AR_Ref,3,0,0,1,0,0,0,0,0,NULL,0,0,0,'',0,0,0,0,CONVERT(DATETIME,'1753-01-01 00:00:00.000'),@cbCreateur,@cbCreationUser);
	
	SELECT @ID = cbMarq FROM @TableMarq;

	INSERT INTO F_ARTCOMPTA (AR_Ref,ACP_Type,ACP_Champ,ACP_ComptaCPT_CompteG,ACP_ComptaCPT_CompteA,ACP_ComptaCPT_Taxe1,ACP_ComptaCPT_Taxe2,ACP_ComptaCPT_Taxe3,ACP_ComptaCPT_Date1,ACP_ComptaCPT_Date2,ACP_ComptaCPT_Date3,ACP_ComptaCPT_TaxeAnc1,ACP_ComptaCPT_TaxeAnc2,ACP_ComptaCPT_TaxeAnc3,ACP_TypeFacture,cbCreateur,cbCreationUser) --OUTPUT inserted.cbMarq INTO @TableMarq 
	VALUES (@AR_Ref,@ACP_Type,@ACP_Champ,@ACP_ComptaCPT_CompteG,@ACP_ComptaCPT_CompteA,@ACP_ComptaCPT_Taxe1,@ACP_ComptaCPT_Taxe2,@ACP_ComptaCPT_Taxe3,@ACP_ComptaCPT_Date1,@ACP_ComptaCPT_Date2,@ACP_ComptaCPT_Date3,@ACP_ComptaCPT_TaxeAnc1,@ACP_ComptaCPT_TaxeAnc2,@ACP_ComptaCPT_TaxeAnc3,@ACP_TypeFacture,@cbCreateur,@cbCreationUser);

END

END

--SELECT * FROM dbo.F_COMPTEG

--dbo.F_ARTCOMPTA
--(
--	AR_Ref varchar(19) NOT NULL,
--	cbAR_Ref  AS (CONVERT(varbinary(20),AR_Ref)),
--	ACP_Type smallint NULL,
--	ACP_Champ smallint NULL,
--	ACP_ComptaCPT_CompteG varchar(13) NULL,
--	ACP_ComptaCPT_CompteA varchar(13) NULL,
--	ACP_ComptaCPT_Taxe1 varchar(5) NULL,
--	ACP_ComptaCPT_Taxe2 varchar(5) NULL,
--	ACP_ComptaCPT_Taxe3 varchar(5) NULL,
--	ACP_ComptaCPT_Date1 datetime NULL,
--	ACP_ComptaCPT_Date2 datetime NULL,
--	ACP_ComptaCPT_Date3 datetime NULL,
--	ACP_ComptaCPT_TaxeAnc1 varchar(5) NULL,
--	ACP_ComptaCPT_TaxeAnc2 varchar(5) NULL,
--	ACP_ComptaCPT_TaxeAnc3 varchar(5) NULL,
--	ACP_TypeFacture smallint NULL,
--	cbProt smallint NULL,
--	cbMarq int IDENTITY(1,1) NOT NULL,
--	cbCreateur char(4) NULL,
--	cbModification datetime NULL,
--	cbReplication int NULL,
--	cbFlag smallint NULL,
--	cbCreation datetime NULL,
--	cbCreationUser uniqueidentifier NULL,