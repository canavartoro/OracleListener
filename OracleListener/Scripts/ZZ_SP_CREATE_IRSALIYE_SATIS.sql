USE [PREPROD]
GO
/****** Object:  StoredProcedure [dbo].[ZZ_SP_CREATE_IRSALIYE_SATIS]    Script Date: 5.12.2021 00:20:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ZZ_SP_CREATE_IRSALIYE_SATIS](
	@DO_Piece varchar(13), -- BELGE NUMARASI
	@DO_Date datetime, -- BELGE TARIHI
	@ENTITY_ID INTEGER, -- CARI ID
	@DO_Tiers varchar(17), -- CARI KODU
	@CT_Intitule varchar(69), -- CARI ADI
	@DE_No int, --DEPO ID
	@AR_Ref varchar(19), -- STOK KODU
	@DL_Design varchar(69), -- STOK ADI
	@DL_Qte numeric(24, 6) --MIKTAR
)
--EXECUTE dbo.ZZ_SP_CREATE_IRSALIYE_SATIS 'SAT08','2021-12-02 00:00:00',444145,'4112GBKHERE','DENEME CARISI 32 54',3146,'F000002','MOTOR 5 KW', 3
AS BEGIN
SET NOCOUNT ON
DECLARE 
	@DO_Domaine smallint =0,
	@DO_Type smallint = 3,
	--@DO_Piece varchar(13) 	=	'SAT2',
	--@DO_Date datetime 	=	'2021-12-01 00:00:00',
	@DO_Ref varchar(17) 	=	'',
	--@DO_Tiers varchar(17) 	=	'4112GBTHARA',
	@CO_No int 	=	0,
	@cbCO_No int =	NULL,
	@DO_Period smallint =	1,
	@DO_Devise smallint =	0,
	@DO_Cours numeric(24, 6) =	0,
	--@DE_No int =	1,
	@cbDE_No int =	@DE_No,
	@LI_No int 	=	0,
	@cbLI_No int =	NULL,
	@CT_NumPayeur varchar(17) =	@DO_Tiers, --'4112GBTHARA',
	@DO_Expedit smallint =	1,
	@DO_NbFacture smallint =	1,
	@DO_BLFact smallint =	0,
	@DO_TxEscompte numeric(24, 6) 	=	0,
	@DO_Reliquat smallint 	=	0,
	@DO_Imprim smallint 	=	0,
	@CA_Num varchar(13) 	=	'',
	@DO_Coord01 varchar(25) 	=	'',
	@DO_Coord02 varchar(25) 	=	'',
	@DO_Coord03 varchar(25) 	=	'',
	@DO_Coord04 varchar(25) 	=	'',
	@DO_Souche smallint 	=	0,
	@DO_DateLivr datetime 	=	@DO_Date, --'2021-12-01 00:00:00',
	@DO_Condition smallint 	=	1,
	@DO_Tarif smallint 	=	1,
	@DO_Colisage smallint 	=	1,
	@DO_TypeColis smallint 	=	1,
	@DO_Transaction smallint 	=	11,
	@DO_Langue smallint 	=	0,
	@DO_Ecart numeric(24, 6) 	=	0,
	@DO_Regime smallint 	=	21,
	@N_CatCompta smallint 	=	1,
	@DO_Ventile smallint 	=	0,
	@AB_No int 	=	0,
	@DO_DebutAbo datetime 	=	'1753-01-01 00:00:00',
	@DO_FinAbo datetime 	=	'1753-01-01 00:00:00',
	@DO_DebutPeriod datetime 	=	'1753-01-01 00:00:00',
	@DO_FinPeriod datetime 	=	'1753-01-01 00:00:00',
	@CG_Num varchar(13) 	=	'41110000',
	@DO_Statut smallint 	=	2,
	@DO_Heure char(9) 	=	'000' + REPLACE(CONVERT(NVARCHAR(8),GETDATE(),114), ':',''),
	@CA_No int 	=	0,
	@cbCA_No int 	=	NULL,
	@CO_NoCaissier int 	=	0,
	@cbCO_NoCaissier int 	=	NULL,
	@DO_Transfere smallint 	=	0,
	@DO_Cloture smallint 	=	0,
	@DO_NoWeb varchar(17) 	=	'',
	@DO_Attente smallint 	=	0,
	@DO_Provenance smallint 	=	0,
	@CA_NumIFRS varchar(13) 	=	'',
	@MR_No int 	=	0,
	@DO_TypeFrais smallint 	=	0,
	@DO_ValFrais numeric(24, 6) 	=	0,
	@DO_TypeLigneFrais smallint 	=	0,
	@DO_TypeFranco smallint 	=	0,
	@DO_ValFranco numeric(24, 6) 	=	0,
	@DO_TypeLigneFranco smallint 	=	0,
	@DO_Taxe1 numeric(24, 6) 	=	0,
	@DO_TypeTaux1 smallint 	=	0,
	@DO_TypeTaxe1 smallint 	=	0,
	@DO_Taxe2 numeric(24, 6) 	=	0,
	@DO_TypeTaux2 smallint 	=	0,
	@DO_TypeTaxe2 smallint 	=	0,
	@DO_Taxe3 numeric(24, 6) 	=	0,
	@DO_TypeTaux3 smallint 	=	0,
	@DO_TypeTaxe3 smallint 	=	0,
	@DO_MajCpta smallint 	=	0,
	@DO_Motif varchar(69) 	=	'',
	@CT_NumCentrale varchar(17) 	=	NULL,
	@DO_Contact varchar(35) 	=	'',
	@DO_FactureElec smallint 	=	0,
	@DO_TypeTransac smallint 	=	0,
	@DO_DateLivrRealisee datetime 	=	'1753-01-01 00:00:00',
	@DO_DateExpedition datetime 	=	'1753-01-01 00:00:00',
	@DO_FactureFrs varchar(35) 	=	'',
	@DO_PieceOrig varchar(13) 	=	'',
	@DO_GUID uniqueidentifier 	=	NULL,
	@DO_EStatut smallint 	=	0,
	@DO_DemandeRegul smallint 	=	0,
	@ET_No int 	=	0,
	@cbET_No int 	=	NULL,
	@DO_Valide smallint 	=	0,
	@DO_Coffre smallint 	=	0,
	@DO_CodeTaxe1 varchar(5) 	=	NULL,
	@DO_CodeTaxe2 varchar(5) 	=	NULL,
	@DO_CodeTaxe3 varchar(5) 	=	NULL,
	@DO_TotalHT numeric(24, 6) 	=	0,
	@DO_StatutBAP smallint 	=	0,
	@DO_Escompte smallint 	=	0,
	@DO_DocType smallint 	=	3,
	@DO_TypeCalcul smallint 	=	0,
	@DO_FactureFile uniqueidentifier 	=	NULL,
	@DO_TotalHTNet numeric(24, 6) 	=	0,
	@DO_TotalTTC numeric(24, 6) 	=	0,
	@DO_NetAPayer numeric(24, 6) 	=	0,
	@DO_MontantRegle numeric(24, 6) 	=	0,
	@DO_RefPaiement uniqueidentifier 	=	NULL,
	@DO_AdressePaiement varchar(255) 	=	'',
	@DO_PaiementLigne smallint 	=	0,
	@DO_MotifDevis smallint 	=	0,
	@DO_Conversion smallint 	=	0,
	@cbCreateur char(4) 	=	'COLU',
	@cbCreationUser uniqueidentifier 	=	'796D40A0-0208-42C6-BFDE-9F52C765962A',
	@cbMarq int = NULL,
	--F_DOCENTETEINFOS
	@DI_Code varchar(13) =	'', 
	@DI_Intitule varchar(35) =	3, 
	@DI_Valeur varchar(101) =	@DO_Tiers, 
	--F_DOCREGL
	@DR_No int 	=	NULL,
	@DR_TypeRegl smallint 	=	2,
	@DR_Date datetime 	=	@DO_Date,
	@DR_Libelle varchar(35) 	=	'',
	@DR_Pourcent numeric(24, 6) 	=	0,
	@DR_Montant numeric(24, 6) 	=	0,
	@DR_MontantDev numeric(24, 6) 	=	0,
	@DR_Equil smallint 	=	1,
	@EC_No int 	=	0,
	@cbEC_No int 	=	NULL,
	@DR_Regle smallint 	=	0,
	@N_Reglement smallint 	=	1,
	@DR_RefPaiement uniqueidentifier 	=	NULL,
	@DR_AdressePaiement varchar(255) 	=	'',
	@DO_PieceAcompte varchar(13) 	=	'',
	--F_DOCLIGNE
	@CT_Num varchar(17) 	=	NULL,
	@DL_PieceBC varchar(13) 	=	'',
	@DL_PieceBL varchar(13) 	=	'',
	@DL_DateBC datetime 	=	@DO_Date,
	@DL_DateBL datetime 	=	@DO_Date,
	@DL_Ligne int 	=	1000,
	@DL_TNomencl smallint 	=	0,
	@DL_TRemPied smallint 	=	0,
	@DL_TRemExep smallint 	=	0,
	--@AR_Ref varchar(19) 	=	'F000002',
	--@DL_Design varchar(69) 	=	'MOTOR 5 KW',
	--@DL_Qte numeric(24, 6) 	=	1,
	@DL_QteBC numeric(24, 6) 	=	@DL_Qte,
	@DL_QteBL numeric(24, 6) 	=	@DL_Qte,
	@DL_PoidsNet numeric(24, 6) 	=	0,
	@DL_PoidsBrut numeric(24, 6) 	=	0,
	@DL_Remise01REM_Valeur numeric(24, 6) 	=	0,
	@DL_Remise01REM_Type smallint 	=	0,
	@DL_Remise02REM_Valeur numeric(24, 6) 	=	0,
	@DL_Remise02REM_Type smallint 	=	0,
	@DL_Remise03REM_Valeur numeric(24, 6) 	=	0,
	@DL_Remise03REM_Type smallint 	=	0,
	@DL_PrixUnitaire numeric(24, 6) 	=	0,
	@DL_PUBC numeric(24, 6) 	=	0,
	@DL_Taxe1 numeric(24, 6) 	=	0,
	@DL_TypeTaux1 smallint 	=	0,
	@DL_TypeTaxe1 smallint 	=	0,
	@DL_Taxe2 numeric(24, 6) 	=	0,
	@DL_TypeTaux2 smallint 	=	0,
	@DL_TypeTaxe2 smallint 	=	0,
	@AG_No1 int 	=	0,
	@AG_No2 int 	=	0,
	@DL_PrixRU numeric(24, 6) 	=	0,
	@DL_CMUP numeric(24, 6) 	=	0,
	@DL_MvtStock smallint 	=	3,
	@DT_No int 	=	0,
	@cbDT_No int 	=	NULL,
	@AF_RefFourniss varchar(19) 	=	'',
	@EU_Enumere varchar(35) 	=	'T',
	@EU_Qte numeric(24, 6) 	=	@DL_Qte,
	@DL_TTC smallint 	=	0,
	@DL_NoRef smallint 	=	1,
	@DL_TypePL smallint 	=	0,
	@DL_PUDevise numeric(24, 6) 	=	0,
	@DL_PUTTC numeric(24, 6) 	=	0,
	@DL_No int 	=	NULL,
	@DL_Taxe3 numeric(24, 6) 	=	0,
	@DL_TypeTaux3 smallint 	=	0,
	@DL_TypeTaxe3 smallint 	=	0,
	@DL_Frais numeric(24, 6) 	=	0,
	@DL_Valorise smallint 	=	1,
	@AR_RefCompose varchar(19) 	=	NULL,
	@DL_NonLivre smallint 	=	0,
	@AC_RefClient varchar(19) 	=	'',
	@DL_MontantHT numeric(24, 6) 	=	0,
	@DL_MontantTTC numeric(24, 6) 	=	0,
	@DL_FactPoids smallint 	=	0,
	@DL_Escompte smallint 	=	0,
	@DL_PiecePL varchar(13) 	=	'',
	@DL_DatePL datetime 	=	@DO_Date,--'2021-12-01 00:00:00',
	@DL_QtePL numeric(24, 6) 	=	@DL_Qte,
	@DL_NoColis varchar(19) 	=	'',
	@DL_NoLink int 	=	0,
	@cbDL_NoLink int 	=	NULL,
	@RP_Code varchar(11) 	=	NULL,
	@DL_QteRessource int 	=	0,
	@DL_DateAvancement DATETIME  =	'1753-01-01 00:00:00',
	@PF_Num varchar(9) 	=	'',
	@DL_CodeTaxe1 varchar(5) 	=	NULL,
	@DL_CodeTaxe2 varchar(5) 	=	NULL,
	@DL_CodeTaxe3 varchar(5) 	=	NULL,
	@DL_PieceOFProd int 	=	0,
	@DL_PieceDE varchar(13) 	=	'',
	@DL_DateDE datetime 	=	@DO_Date, --'2021-12-01 00:00:00',
	@DL_QteDE numeric(24, 6) 	=	@DL_Qte,
	@DL_Operation varchar(11) 	=	'',
	@DL_NoSousTotal int 	=	0,
	--F_DOCLIGNEINFOS
	@DC_Code varchar(13) = NULL,
	@DC_Type smallint = 0,
	@DC_Intitule varchar(35) = NULL,
	@DC_Valeur varchar(101) = NULL,
	@ENTITY_CODE NVARCHAR(20) = NULL

	PRINT @DO_Heure

	SELECT @ENTITY_CODE = CONCAT(N'411',REPLACE(@CT_Intitule, ' ', ''))

	PRINT @ENTITY_CODE

	EXECUTE [dbo].[ZZ_SP_CREATE_COMPTET] @ENTITY_ID,@ENTITY_CODE,@CT_Intitule,@CT_Num OUTPUT

	SELECT @DO_Tiers = @CT_Num, @CT_NumPayeur = @CT_Num, @DI_Valeur = @CT_Num

	EXECUTE [dbo].[ZZ_SP_CREATE_ARTICLE] 0,@AR_Ref,@DL_Design,0,'TON'

BEGIN TRANSACTION

INSERT INTO dbo.F_DOCENTETE (DO_Domaine,DO_Type,DO_Piece,DO_Date,DO_Ref,DO_Tiers,CO_No,cbCO_No,DO_Period,DO_Devise,DO_Cours,DE_No,cbDE_No,LI_No,cbLI_No,CT_NumPayeur,DO_Expedit,DO_NbFacture,DO_BLFact,DO_TxEscompte,DO_Reliquat,DO_Imprim,CA_Num,DO_Coord01,DO_Coord02,DO_Coord03,DO_Coord04,DO_Souche,DO_DateLivr,DO_Condition,DO_Tarif,DO_Colisage,DO_TypeColis,DO_Transaction,DO_Langue,DO_Ecart,DO_Regime,N_CatCompta,DO_Ventile,AB_No,DO_DebutAbo,DO_FinAbo,DO_DebutPeriod,DO_FinPeriod,CG_Num,DO_Statut,DO_Heure,CA_No,cbCA_No,CO_NoCaissier,cbCO_NoCaissier,DO_Transfere,DO_Cloture,DO_NoWeb,DO_Attente,DO_Provenance,CA_NumIFRS,MR_No,DO_TypeFrais,DO_ValFrais,DO_TypeLigneFrais,DO_TypeFranco,DO_ValFranco,DO_TypeLigneFranco,DO_Taxe1,DO_TypeTaux1,DO_TypeTaxe1,DO_Taxe2,DO_TypeTaux2,DO_TypeTaxe2,DO_Taxe3,DO_TypeTaux3,DO_TypeTaxe3,DO_MajCpta,DO_Motif,CT_NumCentrale,DO_Contact,DO_FactureElec,DO_TypeTransac,DO_DateLivrRealisee,DO_DateExpedition,DO_FactureFrs,DO_PieceOrig,DO_GUID,DO_EStatut,DO_DemandeRegul,ET_No,cbET_No,DO_Valide,DO_Coffre,DO_CodeTaxe1,DO_CodeTaxe2,DO_CodeTaxe3,DO_TotalHT,DO_StatutBAP,DO_Escompte,DO_DocType,DO_TypeCalcul,DO_FactureFile,DO_TotalHTNet,DO_TotalTTC,DO_NetAPayer,DO_MontantRegle,DO_RefPaiement,DO_AdressePaiement,DO_PaiementLigne,DO_MotifDevis,DO_Conversion,cbCreateur,cbCreationUser) 
VALUES (@DO_Domaine,@DO_Type,@DO_Piece,@DO_Date,@DO_Ref,@DO_Tiers,@CO_No,@cbCO_No,@DO_Period,@DO_Devise,@DO_Cours,@DE_No,@cbDE_No,@LI_No,@cbLI_No,@CT_NumPayeur,@DO_Expedit,@DO_NbFacture,@DO_BLFact,@DO_TxEscompte,@DO_Reliquat,@DO_Imprim,@CA_Num,@DO_Coord01,@DO_Coord02,@DO_Coord03,@DO_Coord04,@DO_Souche,@DO_DateLivr,@DO_Condition,@DO_Tarif,@DO_Colisage,@DO_TypeColis,@DO_Transaction,@DO_Langue,@DO_Ecart,@DO_Regime,@N_CatCompta,@DO_Ventile,@AB_No,@DO_DebutAbo,@DO_FinAbo,@DO_DebutPeriod,@DO_FinPeriod,@CG_Num,@DO_Statut,@DO_Heure,@CA_No,@cbCA_No,@CO_NoCaissier,@cbCO_NoCaissier,@DO_Transfere,@DO_Cloture,@DO_NoWeb,@DO_Attente,@DO_Provenance,@CA_NumIFRS,@MR_No,@DO_TypeFrais,@DO_ValFrais,@DO_TypeLigneFrais,@DO_TypeFranco,@DO_ValFranco,@DO_TypeLigneFranco,@DO_Taxe1,@DO_TypeTaux1,@DO_TypeTaxe1,@DO_Taxe2,@DO_TypeTaux2,@DO_TypeTaxe2,@DO_Taxe3,@DO_TypeTaux3,@DO_TypeTaxe3,@DO_MajCpta,@DO_Motif,@CT_NumCentrale,@DO_Contact,@DO_FactureElec,@DO_TypeTransac,@DO_DateLivrRealisee,@DO_DateExpedition,@DO_FactureFrs,@DO_PieceOrig,@DO_GUID,@DO_EStatut,@DO_DemandeRegul,@ET_No,@cbET_No,@DO_Valide,@DO_Coffre,@DO_CodeTaxe1,@DO_CodeTaxe2,@DO_CodeTaxe3,@DO_TotalHT,@DO_StatutBAP,@DO_Escompte,@DO_DocType,@DO_TypeCalcul,@DO_FactureFile,@DO_TotalHTNet,@DO_TotalTTC,@DO_NetAPayer,@DO_MontantRegle,@DO_RefPaiement,@DO_AdressePaiement,@DO_PaiementLigne,@DO_MotifDevis,@DO_Conversion,@cbCreateur,@cbCreationUser)
SELECT @cbMarq = SCOPE_IDENTITY()

INSERT INTO F_DOCENTETEINFOS (DO_Type, DO_Piece, DI_Code, DI_Type, DI_Intitule , DI_Valeur)				
SELECT @DO_Type , @DO_Piece, CI_Code, CI_Type , CI_Intitule , CI_Valeur FROM dbo.F_COMPTETINFOS WITH (NOLOCK) 
WHERE cbCT_Num = convert(varbinary(255),@DO_Tiers) AND CI_Domaine = 1 

SELECT @DR_No =  MAX(DR_No) + 1 FROM F_DOCREGL WITH (NOLOCK) --(UPDLOCK,TABLOCK)

INSERT INTO dbo.F_DOCREGL (DR_No,DO_Domaine,DO_Type,DO_Piece,DR_TypeRegl,DR_Date,DR_Libelle,DR_Pourcent,DR_Montant,DR_MontantDev,DR_Equil,EC_No,cbEC_No,DR_Regle,N_Reglement,CA_No,cbCA_No,DO_DocType,DR_RefPaiement,DR_AdressePaiement,DO_PieceAcompte,cbCreateur,cbCreationUser)
VALUES (@DR_No,@DO_Domaine,@DO_Type,@DO_Piece,@DR_TypeRegl,@DR_Date,@DR_Libelle,@DR_Pourcent,@DR_Montant,@DR_MontantDev,@DR_Equil,@EC_No,@cbEC_No,@DR_Regle,@N_Reglement,@CA_No,@cbCA_No,@DO_DocType,@DR_RefPaiement,@DR_AdressePaiement,@DO_PieceAcompte,@cbCreateur,@cbCreationUser)

SELECT @DL_No = MAX(DL_No) + 1 FROM F_DOCLIGNE WITH (NOLOCK) --(UPDLOCK,TABLOCK)

INSERT INTO dbo.F_DOCLIGNE (DO_Domaine,DO_Type,CT_Num,DO_Piece,DL_PieceBC,DL_PieceBL,DO_Date,DL_DateBC,DL_DateBL,DL_Ligne,DO_Ref,DL_TNomencl,DL_TRemPied,DL_TRemExep,AR_Ref,DL_Design,DL_Qte,DL_QteBC,DL_QteBL,DL_PoidsNet,DL_PoidsBrut,DL_Remise01REM_Valeur,DL_Remise01REM_Type,DL_Remise02REM_Valeur,DL_Remise02REM_Type,DL_Remise03REM_Valeur,DL_Remise03REM_Type,DL_PrixUnitaire,DL_PUBC,DL_Taxe1,DL_TypeTaux1,DL_TypeTaxe1,DL_Taxe2,DL_TypeTaux2,DL_TypeTaxe2,CO_No,cbCO_No,AG_No1,AG_No2,DL_PrixRU,DL_CMUP,DL_MvtStock,DT_No,cbDT_No,AF_RefFourniss,EU_Enumere,EU_Qte,DL_TTC,DE_No,cbDE_No,DL_NoRef,DL_TypePL,DL_PUDevise,DL_PUTTC,DL_No,DO_DateLivr,CA_Num,DL_Taxe3,DL_TypeTaux3,DL_TypeTaxe3,DL_Frais,DL_Valorise,AR_RefCompose,DL_NonLivre,AC_RefClient,DL_MontantHT,DL_MontantTTC,DL_FactPoids,DL_Escompte,DL_PiecePL,DL_DatePL,DL_QtePL,DL_NoColis,DL_NoLink,cbDL_NoLink,RP_Code,DL_QteRessource,DL_DateAvancement,PF_Num,DL_CodeTaxe1,DL_CodeTaxe2,DL_CodeTaxe3,DL_PieceOFProd,DL_PieceDE,DL_DateDE,DL_QteDE,DL_Operation,DL_NoSousTotal,CA_No,cbCA_No,DO_DocType,cbCreateur,cbCreationUser)
VALUES (@DO_Domaine,@DO_Type,@CT_Num,@DO_Piece,@DL_PieceBC,@DL_PieceBL,@DO_Date,@DL_DateBC,@DL_DateBL,@DL_Ligne,@DO_Ref,@DL_TNomencl,@DL_TRemPied,@DL_TRemExep,@AR_Ref,@DL_Design,@DL_Qte,@DL_QteBC,@DL_QteBL,@DL_PoidsNet,@DL_PoidsBrut,@DL_Remise01REM_Valeur,@DL_Remise01REM_Type,@DL_Remise02REM_Valeur,@DL_Remise02REM_Type,@DL_Remise03REM_Valeur,@DL_Remise03REM_Type,@DL_PrixUnitaire,@DL_PUBC,@DL_Taxe1,@DL_TypeTaux1,@DL_TypeTaxe1,@DL_Taxe2,@DL_TypeTaux2,@DL_TypeTaxe2,@CO_No,@cbCO_No,@AG_No1,@AG_No2,@DL_PrixRU,@DL_CMUP,@DL_MvtStock,@DT_No,@cbDT_No,@AF_RefFourniss,@EU_Enumere,@EU_Qte,@DL_TTC,@DE_No,@cbDE_No,@DL_NoRef,@DL_TypePL,@DL_PUDevise,@DL_PUTTC,@DL_No,@DO_DateLivr,@CA_Num,@DL_Taxe3,@DL_TypeTaux3,@DL_TypeTaxe3,@DL_Frais,@DL_Valorise,@AR_RefCompose,@DL_NonLivre,@AC_RefClient,@DL_MontantHT,@DL_MontantTTC,@DL_FactPoids,@DL_Escompte,@DL_PiecePL,@DL_DatePL,@DL_QtePL,@DL_NoColis,@DL_NoLink,@cbDL_NoLink,@RP_Code,@DL_QteRessource,@DL_DateAvancement,@PF_Num,@DL_CodeTaxe1,@DL_CodeTaxe2,@DL_CodeTaxe3,@DL_PieceOFProd,@DL_PieceDE,@DL_DateDE,@DL_QteDE,@DL_Operation,@DL_NoSousTotal,@CA_No,@cbCA_No,@DO_DocType,@cbCreateur,@cbCreationUser)

--UPDATE F_ARTSTOCK SET AS_MontSto = @P1,AS_QteSto = @P2,cbCreateur = @P3 
--WHERE cbMarq = @P4',N'@P1 float,@P2 float,@P3 varchar(256),@P4 int',3465000,99,'COLU',6

SET FMTONLY ON 
INSERT INTO dbo.F_DOCLIGNEINFOS (DL_No,DC_Code,DC_Type,DC_Intitule,DC_Valeur)	
SELECT 0,CI_Code,CI_Type ,CI_Intitule ,CI_Valeur FROM F_COMPTETINFOS WITH (NOLOCK) 
WHERE cbCT_Num = convert(varbinary(255),' ') AND CI_Domaine = 2 
SET FMTONLY OFF

COMMIT TRANSACTION

SELECT @cbMarq ID


END

