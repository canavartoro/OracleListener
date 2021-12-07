ALTER PROCEDURE [dbo].[ZZ_SP_CREATE_IRSALIYE_ALIS](
	@DO_Piece VARCHAR(13), --BELGE NUMARASI
	@DO_Date DATETIME, --BELGE TARIHI
	@ENTITY_ID INTEGER,
	@DO_Tiers VARCHAR(17), -- CARI KODU
	@CT_Intitule varchar(69) -- CARI ADI
)
--EXECUTE dbo.ZZ_SP_CREATE_IRSALIYE_ALIS 'BL146941','2021-01-10 00:00:00',1032404,'320 01 0285','AL JAWAD'	
AS BEGIN
SET NOCOUNT ON
DECLARE @DO_Domaine SMALLINT = 1,-- 0 = Vente 1 = Achat 2 = Stock
@DO_Type SMALLINT = 13,-- 3 = Bon de livraison 6 = Facture
--@DO_Piece VARCHAR(13) = 'HSN336',-- BELGE NUMARASI--
--@DO_Date DATETIME = '2021-11-23 00:00:00', -- BELGE NO
@DO_Ref VARCHAR(17) = '', -- REFERANS NUMARASI
--@DO_Tiers VARCHAR(17) = 'YAOOO', -- MÜŞTERİ VEYA TEDARİKÇİ KART ADI
@CO_No INTEGER = NULL, --dbo.F_COLLABORATEUR
@cbCO_No INTEGER = NULL,
@DO_Period SMALLINT = 1, -- STANDART GİBİ. 
@DO_Devise SMALLINT = 0, -- STANDART GİBİ. 
@DO_Cours NUMERIC(24,6) = 0, -- PARA BİRİMİ DÖNÜŞTÜRME ORANI. ŞİMDİLİK STANDART GÖNDERİCEZ.
@DE_No INTEGER = 1, --F_DEPOT TABLOSUNDAKİ DEPONUN De_No ID'si.
@cbDE_No INTEGER = 1, --F_DEPOT TABLOSUNDAKİ DEPONUN De_No ID'si.
@LI_No INTEGER = NULL, --TESLIMAT YERİ AMA BİZ SEÇMİYORUZ BU ALANI, İLERİDE BAKARIZ FARKINA. ALIŞTA İD Yİ 0 GÖNDERDİ
@cbLI_No INTEGER = NULL,--TESLIMAT YERİ AMA BİZ SEÇMİYORUZ BU ALANI, İLERİDE BAKARIZ FARKINA. SATIŞTA İD Yİ 30 GÖNDERDİ.
@CT_NumPayeur VARCHAR(17) = NULL,-- MÜŞTERİ VEYA TEDARİKÇİ KART ADI
@DO_Expedit SMALLINT = 1, -- NAKLİYECİ- BİZİM İRSALİYELERDE BU NAKLİYECİLER VAR ONLARI BURAYA ATARIZ.
@DO_NbFacture SMALLINT = 1,-- FATURA SAYISI STANDART GİBİ. 
@DO_BLFact SMALLINT = 0,--STANDART GİBİ. 
@DO_TxEscompte NUMERIC(24,6) = 0, -- İNDİRİM ORANI STANDART GİBİ.
@DO_Reliquat SMALLINT = 0,--STANDART GİBİ. 
@DO_Imprim SMALLINT = 0,--STANDART GİBİ. 
@CA_Num VARCHAR(13) = NULL,--'953INDE', --ANALITIK HESAP, EKRANDAN SEÇİLİYOR
@DO_Coord01 VARCHAR(25) = '', --AÇIKLAMA
@DO_Coord02 VARCHAR(25) = '',--AÇIKLAMA
@DO_Coord03 VARCHAR(25) = '',--AÇIKLAMA
@DO_Coord04 VARCHAR(25) = '',--AÇIKLAMA
@DO_Souche SMALLINT = 0,--STANDART GİBİ. 
@DO_DateLivr DATETIME = @DO_Date,--TESLIM TARIHI
@DO_Condition SMALLINT = 1,--STANDART GİBİ. 
@DO_Tarif SMALLINT = 1,--STANDART GİBİ. 
@DO_Colisage SMALLINT = 1,--STANDART GİBİ. 
@DO_TypeColis SMALLINT = 1,--STANDART GİBİ. 
@DO_Transaction SMALLINT = 11,--STANDART GİBİ. 
@DO_Langue SMALLINT = 0,-- DİL TANIMI STANDART GİBİ. 0 = Aucune1 = Langue1 2 = Langue2
@DO_Ecart NUMERIC(24,6) = 0,--STANDART GİBİ. 
@DO_Regime SMALLINT = 21,--STANDART GİBİ. ALIŞTA 11 SATIŞTA 21 ATMIŞ. DENEMEK LAZIM.
@N_CatCompta SMALLINT = 1,--STANDART GİBİ. 
@DO_Ventile SMALLINT = 0,--STANDART GİBİ. 
@AB_No INTEGER = 0,--STANDART GİBİ. 
@DO_DebutAbo DATETIME = '1753-01-01 00:00:00',--STANDART GİBİ. 
@DO_FinAbo DATETIME = '1753-01-01 00:00:00',--STANDART GİBİ. 
@DO_DebutPeriod DATETIME = '1753-01-01 00:00:00',--STANDART GİBİ. 
@DO_FinPeriod DATETIME = '1753-01-01 00:00:00',--STANDART GİBİ. 
@CG_Num VARCHAR(13) = '40100000',--HESAP PLANI MUHASEBE KODU ALIŞ=4010000 - SATIŞ=4110000
@DO_Statut SMALLINT = 2, --BELGE DURUMU 2 = Accepté
@DO_Heure CHAR(9) = '000' + REPLACE(CONVERT(NVARCHAR(8),GETDATE(),114), ':',''),--BELGE TIME ZAMANI. dbo.F_COMPTEG
@CA_No INTEGER = 0,--STANDART GİBİ. 
@cbCA_No INTEGER = NULL,--STANDART GİBİ. 
@CO_NoCaissier INTEGER = 0,--STANDART GİBİ. 
@cbCO_NoCaissier INTEGER = NULL,--STANDART GİBİ. 
@DO_Transfere SMALLINT = 0,--STANDART GİBİ. 
@DO_Cloture SMALLINT = 0,--STANDART GİBİ. 
@DO_NoWeb VARCHAR(17) = '',--STANDART GİBİ. 
@DO_Attente SMALLINT = 0,--STANDART GİBİ. 
@DO_Provenance SMALLINT = 0,--STANDART GİBİ. NORMAL FATURA
@CA_NumIFRS VARCHAR(13) = '',--STANDART GİBİ. 
@MR_No INTEGER = 0,--STANDART GİBİ. 
@DO_TypeFrais SMALLINT = 0,--STANDART GİBİ. TOPTAN ÖDEME
@DO_ValFrais NUMERIC(24,6) = 15,--FİYATLAR BAKICAM TEKRAR
@DO_TypeLigneFrais SMALLINT = 0,--FİYATLAR BAKICAM TEKRAR
@DO_TypeFranco SMALLINT = 0,--FİYATLAR BAKICAM TEKRAR
@DO_ValFranco NUMERIC(24,6) = 2500,--FİYATLAR BAKICAM TEKRAR
@DO_TypeLigneFranco SMALLINT = 0,--FİYATLAR BAKICAM TEKRAR
@DO_Taxe1 NUMERIC(24,6) = 20,--FİYATLAR BAKICAM TEKRAR
@DO_TypeTaux1 SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_TypeTaxe1 SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_Taxe2 NUMERIC(24,6) = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_TypeTaux2 SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_TypeTaxe2 SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_Taxe3 NUMERIC(24,6) = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_TypeTaux3 SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_TypeTaxe3 SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_MajCpta SMALLINT = 0,--STANDART GİBİ.  VERİ ORANLARI
@DO_Motif VARCHAR(69) = '',--STANDART GİBİ.  VERİ ORANLARI
@CT_NumCentrale VARCHAR(17) = NULL,--STANDART GİBİ. 
@DO_Contact VARCHAR(35) = '',--STANDART GİBİ. 
@DO_FactureElec SMALLINT = 0, --STANDART GİBİ. 
@DO_TypeTransac SMALLINT = 0,--STANDART GİBİ. 
@DO_DateLivrRealisee DATETIME = '1753-01-01 00:00:00',--STANDART GİBİ. 
@DO_DateExpedition DATETIME = '1753-01-01 00:00:00',--STANDART GİBİ. 
@DO_FactureFrs VARCHAR(35) = '',--STANDART GİBİ. 
@DO_PieceOrig VARCHAR(13) = '',--STANDART GİBİ. 
@DO_GUID UNIQUEIDENTIFIER = NULL,--STANDART GİBİ. 
@DO_EStatut SMALLINT = 0,--STANDART GİBİ. 
@DO_DemandeRegul SMALLINT = 0,--STANDART GİBİ. 
@ET_No INTEGER = 0,--STANDART GİBİ. 
@cbET_No INTEGER = NULL,--STANDART GİBİ. 
@DO_Valide SMALLINT = 0, --STANDART GİBİ. 
@DO_Coffre SMALLINT = 0,--STANDART GİBİ. 
@DO_CodeTaxe1 VARCHAR(5) = NULL,--ALIŞTA = D20 - SATIŞTA = C20 OLARAK ATMIŞ. dbo.F_TAXE
@DO_CodeTaxe2 VARCHAR(5) = NULL,--STANDART GİBİ. 
@DO_CodeTaxe3 VARCHAR(5) = NULL,--STANDART GİBİ. 
@DO_TotalHT NUMERIC(24,6) = 0,--KDV HARİÇ TOPLAM FİYAT
@DO_StatutBAP SMALLINT = 0,--STANDART GİBİ. 
@DO_Escompte SMALLINT = 1,--STANDART GİBİ. 
@DO_DocType SMALLINT = 3,--3 = Bon de livraison - 6 = Facture
@DO_TypeCalcul SMALLINT = 0,--STANDART GİBİ. 
@DO_FactureFile UNIQUEIDENTIFIER = NULL,--STANDART GİBİ. 
@DO_TotalHTNet NUMERIC(24,6) = 0,--FİYATLAR BAKICAM TEKRAR
@DO_TotalTTC NUMERIC(24,6) = 0,--FİYATLAR BAKICAM TEKRAR
@DO_NetAPayer NUMERIC(24,6) = 0,--FİYATLAR BAKICAM TEKRAR
@DO_MontantRegle NUMERIC(24,6) = 0,--FİYATLAR BAKICAM TEKRAR
@DO_RefPaiement UNIQUEIDENTIFIER = NULL,--STANDART GİBİ. 
@DO_AdressePaiement VARCHAR(255) = '',--STANDART GİBİ. 
@DO_PaiementLigne SMALLINT = 0,--STANDART GİBİ. 
@DO_MotifDevis SMALLINT = 0,--STANDART GİBİ. 
@DO_Conversion SMALLINT = 0,--STANDART GİBİ. 
@cbCreateur CHAR(4) = 'COLU',
@cbCreationUser UNIQUEIDENTIFIER = '77384016-921F-472F-B56D-1D563B7DDF3C',
@DR_No INTEGER = 99, --BELGE ID
@DR_TypeRegl SMALLINT = 3,-- ÖDEME TURU STANDART GİBİ. 
@DR_Date DATETIME = @DO_Date,--ÖDEME TARİHİ
@DR_Libelle VARCHAR(35) = '',--STANDART GİBİ. 
@DR_Pourcent NUMERIC(24,6) = 0,--STANDART GİBİ. 
@DR_Montant NUMERIC(24,6) = 0,--STANDART GİBİ. 
@DR_MontantDev NUMERIC(24,6) = 0,--STANDART GİBİ. 
@DR_Equil SMALLINT = 1,--STANDART GİBİ. 
@EC_No INTEGER = 0,--STANDART GİBİ. 
@cbEC_No INTEGER = NULL,--STANDART GİBİ. 
@DR_Regle SMALLINT = 0,--STANDART GİBİ. 
@N_Reglement SMALLINT = 1,--STANDART GİBİ. 
@DR_RefPaiement UNIQUEIDENTIFIER = NULL,--STANDART GİBİ. 
@DR_AdressePaiement VARCHAR(255) = '',--STANDART GİBİ. 
@DO_PieceAcompte VARCHAR(13) = '',--STANDART GİBİ
@F_DOCREGLID INTEGER = NULL,@F_DOCENTETEID INTEGER = NULL, @ENTITY_CODE NVARCHAR(20) = NULL, @CT_Num varchar(17) = NULL

SELECT @ENTITY_CODE = N'401' + REPLACE(@DO_Tiers, ' ', '')

EXECUTE [dbo].[ZZ_SP_CREATE_COMPTET] @ENTITY_ID,@ENTITY_CODE,@CT_Intitule,1,@CT_Num OUTPUT

SELECT @CT_NumPayeur = @CT_Num, @DO_Tiers = @CT_Num

DECLARE @TableMarq TABLE (cbMarq int);

INSERT INTO F_DOCENTETE (DO_Domaine,DO_Type,DO_Piece,DO_Date,DO_Ref,DO_Tiers,CO_No,cbCO_No,DO_Period,DO_Devise,DO_Cours,DE_No,cbDE_No,LI_No,cbLI_No,CT_NumPayeur,DO_Expedit,DO_NbFacture,DO_BLFact,DO_TxEscompte,DO_Reliquat,DO_Imprim,CA_Num,DO_Coord01,DO_Coord02,DO_Coord03,DO_Coord04,DO_Souche,DO_DateLivr,DO_Condition,DO_Tarif,DO_Colisage,DO_TypeColis,DO_Transaction,DO_Langue,DO_Ecart,DO_Regime,N_CatCompta,DO_Ventile,AB_No,DO_DebutAbo,DO_FinAbo,DO_DebutPeriod,DO_FinPeriod,CG_Num,DO_Statut,DO_Heure,CA_No,cbCA_No,CO_NoCaissier,cbCO_NoCaissier,DO_Transfere,DO_Cloture,DO_NoWeb,DO_Attente,DO_Provenance,CA_NumIFRS,MR_No,DO_TypeFrais,DO_ValFrais,DO_TypeLigneFrais,DO_TypeFranco,DO_ValFranco,DO_TypeLigneFranco,DO_Taxe1,DO_TypeTaux1,DO_TypeTaxe1,DO_Taxe2,DO_TypeTaux2,DO_TypeTaxe2,DO_Taxe3,DO_TypeTaux3,DO_TypeTaxe3,DO_MajCpta,DO_Motif,CT_NumCentrale,DO_Contact,DO_FactureElec,DO_TypeTransac,DO_DateLivrRealisee,DO_DateExpedition,DO_FactureFrs,DO_PieceOrig,DO_GUID,DO_EStatut,DO_DemandeRegul,ET_No,cbET_No,DO_Valide,DO_Coffre,DO_CodeTaxe1,DO_CodeTaxe2,DO_CodeTaxe3,DO_TotalHT,DO_StatutBAP,DO_Escompte,DO_DocType,DO_TypeCalcul,DO_FactureFile,DO_TotalHTNet,DO_TotalTTC,DO_NetAPayer,DO_MontantRegle,DO_RefPaiement,DO_AdressePaiement,DO_PaiementLigne,DO_MotifDevis,DO_Conversion,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq VALUES (@DO_Domaine,@DO_Type,@DO_Piece,@DO_Date,@DO_Ref,@DO_Tiers,@CO_No,@cbCO_No,@DO_Period,@DO_Devise,@DO_Cours,@DE_No,@cbDE_No,@LI_No,@cbLI_No,@CT_NumPayeur,@DO_Expedit,@DO_NbFacture,@DO_BLFact,@DO_TxEscompte,@DO_Reliquat,@DO_Imprim,@CA_Num,@DO_Coord01,@DO_Coord02,@DO_Coord03,@DO_Coord04,@DO_Souche,@DO_DateLivr,@DO_Condition,@DO_Tarif,@DO_Colisage,@DO_TypeColis,@DO_Transaction,@DO_Langue,@DO_Ecart,@DO_Regime,@N_CatCompta,@DO_Ventile,@AB_No,@DO_DebutAbo,@DO_FinAbo,@DO_DebutPeriod,@DO_FinPeriod,@CG_Num,@DO_Statut,@DO_Heure,@CA_No,@cbCA_No,@CO_NoCaissier,@cbCO_NoCaissier,@DO_Transfere,@DO_Cloture,@DO_NoWeb,@DO_Attente,@DO_Provenance,@CA_NumIFRS,@MR_No,@DO_TypeFrais,@DO_ValFrais,@DO_TypeLigneFrais,@DO_TypeFranco,@DO_ValFranco,@DO_TypeLigneFranco,@DO_Taxe1,@DO_TypeTaux1,@DO_TypeTaxe1,@DO_Taxe2,@DO_TypeTaux2,@DO_TypeTaxe2,@DO_Taxe3,@DO_TypeTaux3,@DO_TypeTaxe3,@DO_MajCpta,@DO_Motif,@CT_NumCentrale,@DO_Contact,@DO_FactureElec,@DO_TypeTransac,@DO_DateLivrRealisee,@DO_DateExpedition,@DO_FactureFrs,@DO_PieceOrig,@DO_GUID,@DO_EStatut,@DO_DemandeRegul,@ET_No,@cbET_No,@DO_Valide,@DO_Coffre,@DO_CodeTaxe1,@DO_CodeTaxe2,@DO_CodeTaxe3,@DO_TotalHT,@DO_StatutBAP,@DO_Escompte,@DO_DocType,@DO_TypeCalcul,@DO_FactureFile,@DO_TotalHTNet,@DO_TotalTTC,@DO_NetAPayer,@DO_MontantRegle,@DO_RefPaiement,@DO_AdressePaiement,@DO_PaiementLigne,@DO_MotifDevis,@DO_Conversion,@cbCreateur,@cbCreationUser);

SELECT @F_DOCENTETEID = cbMarq FROM @TableMarq;

INSERT INTO F_DOCENTETEINFOS (DO_Type, DO_Piece, DI_Code, DI_Type, DI_Intitule , DI_Valeur)
SELECT @DO_Type , @DO_Piece, CI_Code, CI_Type , CI_Intitule , CI_Valeur FROM F_COMPTETINFOS WHERE cbCT_Num = convert(varbinary(255),@DO_Tiers) AND CI_Domaine = 1 


SET FMTONLY ON 
INSERT INTO F_DOCENTETEINFOS (DO_Type, DO_Piece, DI_Code, DI_Type, DI_Intitule , DI_Valeur)
SELECT 0 , ' ', CI_Code, CI_Type , CI_Intitule , CI_Valeur FROM F_COMPTETINFOS WHERE cbCT_Num = convert(varbinary(255),' ') AND CI_Domaine = 1 
SET FMTONLY OFF

SELECT @DR_No = MAX(DR_No) + 1 FROM F_DOCREGL --WITH (UPDLOCK,TABLOCK)

DECLARE @TableMarq2 TABLE (cbMarq int);
INSERT INTO F_DOCREGL (DR_No,DO_Domaine,DO_Type,DO_Piece,DR_TypeRegl,DR_Date,DR_Libelle,DR_Pourcent,DR_Montant,DR_MontantDev,DR_Equil,EC_No,cbEC_No,DR_Regle,N_Reglement,CA_No,cbCA_No,DO_DocType,DR_RefPaiement,DR_AdressePaiement,DO_PieceAcompte,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq2 VALUES (@DR_No,@DO_Domaine,@DO_Type,@DO_Piece,@DR_TypeRegl,@DR_Date,@DR_Libelle,@DR_Pourcent,@DR_Montant,@DR_MontantDev,@DR_Equil,@EC_No,@cbEC_No,@DR_Regle,@N_Reglement,@CA_No,@cbCA_No,@DO_DocType,@DR_RefPaiement,@DR_AdressePaiement,@DO_PieceAcompte,@cbCreateur,@cbCreationUser);
SELECT @F_DOCREGLID = cbMarq FROM @TableMarq2;

--exec CB_LockRecord 'F_DOCENTETE',3,@F_DOCENTETEID

--exec CB_UnLockRecord 'F_DOCENTETE',3,80

SET NOCOUNT OFF

SELECT @F_DOCENTETEID ID

--exec CB_EqGreaterIAE_REF 0x47

END