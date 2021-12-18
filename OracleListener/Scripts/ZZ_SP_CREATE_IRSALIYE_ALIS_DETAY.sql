

ALTER PROCEDURE [dbo].[ZZ_SP_CREATE_IRSALIYE_ALIS_DETAY](
	@DO_Piece varchar(13), -- BELGE NUMARASI
	@DO_Date datetime, -- BELGE TARIHI
	@CT_Num VARCHAR(17), -- CARI KODU
	@DE_No int, --DEPO ID
	@AR_Ref varchar(19), -- STOK KODU
	@DL_Design varchar(69), -- STOK ADI
	@DL_Qte numeric(24, 6), --MIKTAR
	@UnitPrice FLOAT,
	@ACP_Alis VARCHAR(13)
)
--EXECUTE dbo.ZZ_SP_CREATE_IRSALIYE_ALIS_DETAY 'BL146941','2021-01-10 00:00:00',3121,'F014252','KABLO UCU- IZOLE 4-6/1 KUTU 100 ADET',1,14000
AS BEGIN
SET NOCOUNT ON
DECLARE 
	@DO_Domaine smallint =1,
	@DO_Type smallint = 13,
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
	@CG_Num varchar(13) 	=	'4010000',
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
	@DO_TotalHT numeric(24, 6) 	=	@DL_Qte * @UnitPrice, --BELGE TUTAR
	@DO_StatutBAP smallint 	=	0,
	@DO_Escompte smallint 	=	0,
	@DO_DocType smallint 	=	3,
	@DO_TypeCalcul smallint 	=	0,
	@DO_FactureFile uniqueidentifier 	=	NULL,
	@DO_TotalHTNet numeric(24, 6) 	=	@DL_Qte * @UnitPrice, --KDV HARIC BELGE TUTAR
	@DO_TotalTTC numeric(24, 6) 	=	@DL_Qte * @UnitPrice, --KDV DAHIL BELGE TUTAR
	@DO_NetAPayer numeric(24, 6) 	=	@DL_Qte * @UnitPrice, --BELGE TUTAR
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
	--F_DOCREGL
	@DR_No int 	=	NULL,
	@DR_TypeRegl smallint 	=	2,
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
	--@CT_Num varchar(17) 	=	NULL,
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
	@DL_PrixUnitaire numeric(24, 6) 	=	@UnitPrice,
	@DL_PUBC numeric(24, 6) 	=	@UnitPrice,
	@DL_Taxe1 numeric(24, 6) 	=	18,
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
	@DL_MontantHT numeric(24, 6) 	=	@UnitPrice * @DL_Qte,	--KDV HARIC TOPLAM TUTAR
	@DL_MontantTTC numeric(24, 6) 	=	@UnitPrice * @DL_Qte, --KDV DAHIL TOPLAM TUTAR
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
	@DL_CodeTaxe1 varchar(5) 	=	'TDA',
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
	@ENTITY_CODE NVARCHAR(20) = NULL,
	---F_ARTSTOCK
	@F_ARTSTOCK_cbMarq INT = NULL,
	@AS_QteSto numeric(24,6) = NULL,
	@AS_MontSto numeric(24,6) = NULL,
	@ERROR_MSG NVARCHAR(MAX) = NULL,
	@ACP_Satis VARCHAR(13) = NULL

	EXECUTE [dbo].[ZZ_SP_CREATE_ARTICLE] 0,@AR_Ref,@DL_Design,0,'TON',@ACP_Alis, @ACP_Satis

	IF @@ERROR <> 0 BEGIN
		RETURN
	END

SELECT TOP 1 @F_ARTSTOCK_cbMarq = cbMarq, @AS_QteSto = AS_QteSto, @AS_MontSto = AS_MontSto FROM F_ARTSTOCK WITH (NOLOCK) WHERE AR_Ref = @AR_Ref AND DE_No = @DE_No

BEGIN TRANSACTION

SELECT @DL_No = MAX(DL_No) + 1 FROM F_DOCLIGNE WITH (NOLOCK) --(UPDLOCK,TABLOCK)

INSERT INTO dbo.F_DOCLIGNE (DO_Domaine,DO_Type,CT_Num,DO_Piece,DL_PieceBC,DL_PieceBL,DO_Date,DL_DateBC,DL_DateBL,DL_Ligne,DO_Ref,DL_TNomencl,DL_TRemPied,DL_TRemExep,AR_Ref,DL_Design,DL_Qte,DL_QteBC,DL_QteBL,DL_PoidsNet,DL_PoidsBrut,DL_Remise01REM_Valeur,DL_Remise01REM_Type,DL_Remise02REM_Valeur,DL_Remise02REM_Type,DL_Remise03REM_Valeur,DL_Remise03REM_Type,DL_PrixUnitaire,DL_PUBC,DL_Taxe1,DL_TypeTaux1,DL_TypeTaxe1,DL_Taxe2,DL_TypeTaux2,DL_TypeTaxe2,CO_No,cbCO_No,AG_No1,AG_No2,DL_PrixRU,DL_CMUP,DL_MvtStock,DT_No,cbDT_No,AF_RefFourniss,EU_Enumere,EU_Qte,DL_TTC,DE_No,cbDE_No,DL_NoRef,DL_TypePL,DL_PUDevise,DL_PUTTC,DL_No,DO_DateLivr,CA_Num,DL_Taxe3,DL_TypeTaux3,DL_TypeTaxe3,DL_Frais,DL_Valorise,AR_RefCompose,DL_NonLivre,AC_RefClient,DL_MontantHT,DL_MontantTTC,DL_FactPoids,DL_Escompte,DL_PiecePL,DL_DatePL,DL_QtePL,DL_NoColis,DL_NoLink,cbDL_NoLink,RP_Code,DL_QteRessource,DL_DateAvancement,PF_Num,DL_CodeTaxe1,DL_CodeTaxe2,DL_CodeTaxe3,DL_PieceOFProd,DL_PieceDE,DL_DateDE,DL_QteDE,DL_Operation,DL_NoSousTotal,CA_No,cbCA_No,DO_DocType,cbCreateur,cbCreationUser)
VALUES (@DO_Domaine,@DO_Type,@CT_Num,@DO_Piece,@DL_PieceBC,@DL_PieceBL,@DO_Date,@DL_DateBC,@DL_DateBL,@DL_Ligne,@DO_Ref,@DL_TNomencl,@DL_TRemPied,@DL_TRemExep,@AR_Ref,@DL_Design,@DL_Qte,@DL_QteBC,@DL_QteBL,@DL_PoidsNet,@DL_PoidsBrut,@DL_Remise01REM_Valeur,@DL_Remise01REM_Type,@DL_Remise02REM_Valeur,@DL_Remise02REM_Type,@DL_Remise03REM_Valeur,@DL_Remise03REM_Type,@DL_PrixUnitaire,@DL_PUBC,@DL_Taxe1,@DL_TypeTaux1,@DL_TypeTaxe1,@DL_Taxe2,@DL_TypeTaux2,@DL_TypeTaxe2,@CO_No,@cbCO_No,@AG_No1,@AG_No2,@DL_PrixRU,@DL_CMUP,@DL_MvtStock,@DT_No,@cbDT_No,@AF_RefFourniss,@EU_Enumere,@EU_Qte,@DL_TTC,@DE_No,@cbDE_No,@DL_NoRef,@DL_TypePL,@DL_PUDevise,@DL_PUTTC,@DL_No,@DO_DateLivr,@CA_Num,@DL_Taxe3,@DL_TypeTaux3,@DL_TypeTaxe3,@DL_Frais,@DL_Valorise,@AR_RefCompose,@DL_NonLivre,@AC_RefClient,@DL_MontantHT,@DL_MontantTTC,@DL_FactPoids,@DL_Escompte,@DL_PiecePL,@DL_DatePL,@DL_QtePL,@DL_NoColis,@DL_NoLink,@cbDL_NoLink,@RP_Code,@DL_QteRessource,@DL_DateAvancement,@PF_Num,@DL_CodeTaxe1,@DL_CodeTaxe2,@DL_CodeTaxe3,@DL_PieceOFProd,@DL_PieceDE,@DL_DateDE,@DL_QteDE,@DL_Operation,@DL_NoSousTotal,@CA_No,@cbCA_No,@DO_DocType,@cbCreateur,@cbCreationUser)

IF @@ERROR <> 0 BEGIN
	ROLLBACK TRANSACTION
	RETURN
END

IF @F_ARTSTOCK_cbMarq IS NULL BEGIN
	
	DECLARE @AS_QteMini numeric(24,6) = 0, @AS_QteMaxi numeric(24,6) = 0, @AS_QteRes numeric(24,6) = 0, @AS_QteCom numeric(24,6) = 0,
	@AS_Principal smallint  = 0,@AS_QteResCM numeric(24,6)  = 0,@AS_QteComCM numeric(24,6)  = 0,@AS_QtePrepa numeric(24,6)  = 0,
	@DP_NoPrincipal int = @DE_No, @cbDP_NoPrincipal int = @DE_No, @DP_NoControle int = 0, @cbDP_NoControle int = @DE_No, @AS_QteAControler numeric(24,6) = 0,
	@AS_Mouvemente smallint = 0

	SELECT @AS_QteSto = ISNULL(@AS_QteSto,0) + @DL_Qte, @AS_MontSto = (ISNULL(@AS_QteSto,0) + @DL_Qte) * @UnitPrice

	--SELECT DP_NoPrincipal = @DP_NoPrincipal FROM dbo.F_DEPOT WITH (NOLOCK) WHERE DE_No = @DE_No

	DECLARE @TableMarq TABLE (cbMarq int);
	INSERT INTO F_ARTSTOCK (AR_Ref,DE_No,AS_QteMini,AS_QteMaxi,AS_MontSto,AS_QteSto,AS_QteRes,AS_QteCom,AS_Principal,AS_QteResCM,AS_QteComCM,AS_QtePrepa,DP_NoPrincipal,cbDP_NoPrincipal,DP_NoControle,cbDP_NoControle,AS_QteAControler,AS_Mouvemente,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq 
	VALUES (@AR_Ref,@DE_No,@AS_QteMini,@AS_QteMaxi,@AS_MontSto,@AS_QteSto,@AS_QteRes,@AS_QteCom,@AS_Principal,@AS_QteResCM,@AS_QteComCM,@AS_QtePrepa,@DP_NoPrincipal,@cbDP_NoPrincipal,@DP_NoControle,@cbDP_NoControle,@AS_QteAControler,@AS_Mouvemente,@cbCreateur,@cbCreationUser);
	SELECT @F_ARTSTOCK_cbMarq = cbMarq FROM @TableMarq;
	
END ELSE BEGIN

UPDATE dbo.F_ARTSTOCK SET AS_MontSto/*TUTAR*/ = (ISNULL(@AS_QteSto,0) + @DL_Qte) * @UnitPrice, AS_QteSto/*STOK BAKİYE*/ = ISNULL(@AS_QteSto,0) + @DL_Qte, cbCreateur = @cbCreateur WHERE cbMarq = @F_ARTSTOCK_cbMarq

END


SET FMTONLY ON 
INSERT INTO dbo.F_DOCLIGNEINFOS (DL_No,DC_Code,DC_Type,DC_Intitule,DC_Valeur)	
SELECT 0,CI_Code,CI_Type ,CI_Intitule ,CI_Valeur FROM F_COMPTETINFOS WITH (NOLOCK) 
WHERE cbCT_Num = convert(varbinary(255),' ') AND CI_Domaine = 2 
SET FMTONLY OFF

COMMIT TRANSACTION

SELECT @cbMarq ID

END

