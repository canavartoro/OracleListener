ALTER PROCEDURE [dbo].[ZZ_SP_CREATE_COMPTET](
	@ENTITY_ID INTEGER,
	@ENTITY_CODE NVARCHAR(20),
	@ENTITY_NAME NVARCHAR(100),
	@CT_Type smallint,
	@CT_Num varchar(17) OUTPUT
)
AS BEGIN
DECLARE
--@CT_Num varchar(17) = 	REPLACE(@ENTITY_CODE, ' ', ''),
@CT_Intitule varchar(69) = 	@ENTITY_NAME,
--@CT_Type smallint =	0,
@CG_NumPrinc varchar(13) = 	'41100000',
@CT_Qualite varchar(17) = 	'',
@CT_Classement varchar(17) = 	@ENTITY_NAME,
@CT_Contact varchar(35) = 	'',
@CT_Adresse varchar(35) = 	'',
@CT_Complement varchar(35) = 	'',
@CT_CodePostal varchar(9) = 	'',
@CT_Ville varchar(35) = 	'',
@CT_CodeRegion varchar(25) = 	'',
@CT_Pays varchar(35) = 	'COTE D''IVOIRE',
@CT_Raccourci varchar(7) = 	'',
@BT_Num smallint =	0,
@N_Devise smallint = 0,
@CT_Ape varchar(7) = 	'',
@CT_Identifiant varchar(25) = 	'',
@CT_Siret varchar(15) = 	'',
@CT_Statistique01 varchar(21) = 	'',
@CT_Statistique02 varchar(21) = 	'',
@CT_Statistique03 varchar(21) = 	'',
@CT_Statistique04 varchar(21) = 	'',
@CT_Statistique05 varchar(21) = 	'',
@CT_Statistique06 varchar(21) = 	'',
@CT_Statistique07 varchar(21) = 	'',
@CT_Statistique08 varchar(21) = 	'',
@CT_Statistique09 varchar(21) = 	'',
@CT_Statistique10 varchar(21) = 	'',
@CT_Commentaire varchar(35) = 	'',
@CT_Encours numeric(24,6) = 	0,
@CT_Assurance numeric(24,6) = 	0,
@CT_NumPayeur varchar(17) = 	@ENTITY_CODE,
@N_Risque smallint =	1,
@CO_No int =	0,
@cbCO_No int =	NULL,
@N_CatTarif smallint =	1,
@CT_Taux01 numeric(24,6) = 	0,
@CT_Taux02 numeric(24,6) = 	0,
@CT_Taux03 numeric(24,6) = 	0,
@CT_Taux04 numeric(24,6) = 	0,
@N_CatCompta smallint =	1,
@N_Period smallint =	1,
@CT_Facture smallint =	1,
@CT_BLFact smallint =	0,
@CT_Langue smallint =	0,
@N_Expedition smallint =	1,
@N_Condition smallint =	1,
@CT_Saut smallint =	1,
@CT_Lettrage smallint =	1,
@CT_ValidEch smallint =	0,
@CT_Sommeil smallint =	0,
@DE_No int =	0,
@cbDE_No int =	NULL,
@CT_ControlEnc smallint =	0,
@CT_NotRappel smallint =	0,
@N_Analytique smallint =	0,
@cbN_Analytique smallint =	NULL,
@CA_Num varchar(13) = 	NULL,
@CT_Telephone varchar(21) = 	'',
@CT_Telecopie varchar(21) = 	'',
@CT_EMail varchar(69) = 	'',
@CT_Site varchar(69) = 	'',
@CT_Coface varchar(25) = 	'',
@CT_Surveillance smallint =	0,
@CT_SvDateCreate datetime =	'1753-01-01 00:00:00',
@CT_SvFormeJuri varchar(33) = 	'',
@CT_SvEffectif varchar(11) = 	'',
@CT_SvCA numeric(24,6) = 	0,
@CT_SvResultat numeric(24,6) = 	0,
@CT_SvIncident smallint =	0,
@CT_SvDateIncid datetime =	'1753-01-01 00:00:00',
@CT_SvPrivil smallint =	0,
@CT_SvRegul varchar(3) = 	'',
@CT_SvCotation varchar(5) = 	'',
@CT_SvDateMaj datetime 	= '1753-01-01 00:00:00',
@CT_SvObjetMaj varchar(61) = 	'',
@CT_SvDateBilan datetime =	'1753-01-01 00:00:00',
@CT_SvNbMoisBilan smallint =	0,
@N_AnalytiqueIFRS smallint =	0,
@cbN_AnalytiqueIFRS smallint =	NULL,
@CA_NumIFRS varchar(13) = 	NULL,
@CT_PrioriteLivr smallint =	0,
@CT_LivrPartielle smallint = 0,
@MR_No int =	0,
@cbMR_No int =	NULL,
@CT_NotPenal smallint =	0,
@EB_No int = 0,
@cbEB_No int =	NULL,
@CT_NumCentrale varchar(17) = 	NULL,
@CT_DateFermeDebut datetime =	'1753-01-01 00:00:00',
@CT_DateFermeFin datetime =	'1753-01-01 00:00:00',
@CT_FactureElec smallint =	0,
@CT_TypeNIF smallint = 0,
@CT_RepresentInt varchar(35) = 	'',
@CT_RepresentNIF varchar(25) = 	'',
@CT_EdiCodeType smallint =	0,
@CT_EdiCode varchar(23) = 	'',
@CT_EdiCodeSage varchar(9) = 	'',
@CT_ProfilSoc smallint =	0,
@CT_StatutContrat smallint =	0,
@CT_DateMAJ datetime =	'1753-01-01 00:00:00',
@CT_EchangeRappro smallint =	0,
@CT_EchangeCR smallint =	1,
@PI_NoEchange int =	0,
@cbPI_NoEchange int =	NULL,
@CT_BonAPayer smallint =	0,
@CT_DelaiTransport smallint =	0,
@CT_DelaiAppro smallint =	0,
@CT_LangueISO2 varchar(3) = 	'',
@CT_AnnulationCR smallint =	1,
@CT_Facebook varchar(35) = 	'',
@CT_LinkedIn varchar(35) = 	'',
@CT_ExclureTrait smallint =	0,
@CT_GDPR smallint =	0,
@CT_Prospect smallint =	0,
@CT_OrderDay01 smallint =	1,
@CT_OrderDay02 smallint =	1,
@CT_OrderDay03 smallint =	1,
@CT_OrderDay04 smallint =	1,
@CT_OrderDay05 smallint =	1,
@CT_OrderDay06 smallint =	0,
@CT_OrderDay07 smallint =	0,
@CT_DeliveryDay01 smallint = 1,
@CT_DeliveryDay02 smallint = 1,
@CT_DeliveryDay03 smallint = 1,
@CT_DeliveryDay04 smallint = 1,
@CT_DeliveryDay05 smallint = 1,
@CT_DeliveryDay06 smallint = 0,
@CT_DeliveryDay07 smallint = 0,
@CAL_No int =	2,
@cbCAL_No int =	2,
@cbCreateur char(4) = 	'MA30',
@cbCreationUser uniqueidentifier =	'C762D6AF-9FC8-492B-851C-F476E0ED10AC',
@cbMarq int = NULL,--ben ekledim.
@LI_No INT = NULL

SET NOCOUNT ON

SELECT @ENTITY_CODE = REPLACE(@ENTITY_CODE,' ',''), @CT_NumPayeur = REPLACE(@ENTITY_CODE,' ','')

SELECT TOP 1 @CT_Num = CT_Num, @CT_Qualite = CT_Qualite FROM dbo.F_COMPTET WITH (NOLOCK) WHERE CT_Qualite = CAST(@ENTITY_ID AS varchar(17)) OR CT_Num = @ENTITY_CODE

PRINT @CT_Num

IF @CT_Num IS NULL BEGIN

PRINT 'YOK'

SELECT @CT_Num = REPLACE(@ENTITY_CODE, ' ', ''), @CT_NumPayeur = REPLACE(@ENTITY_CODE, ' ', ''), @CT_Qualite = CAST(@ENTITY_ID AS varchar(17))

PRINT @CT_Num + ' <--> ' + @CT_NumPayeur

DECLARE @TableMarq TABLE (cbMarq int);
INSERT INTO F_COMPTET (CT_Num,CT_Intitule,CT_Type,CG_NumPrinc,CT_Qualite,CT_Classement,CT_Contact,CT_Adresse,CT_Complement,CT_CodePostal,CT_Ville,CT_CodeRegion,CT_Pays,CT_Raccourci,BT_Num,N_Devise,CT_Ape,CT_Identifiant,CT_Siret,CT_Statistique01,CT_Statistique02,CT_Statistique03,CT_Statistique04,CT_Statistique05,CT_Statistique06,CT_Statistique07,CT_Statistique08,CT_Statistique09,CT_Statistique10,CT_Commentaire,CT_Encours,CT_Assurance,CT_NumPayeur,N_Risque,CO_No,cbCO_No,N_CatTarif,CT_Taux01,CT_Taux02,CT_Taux03,CT_Taux04,N_CatCompta,N_Period,CT_Facture,CT_BLFact,CT_Langue,N_Expedition,N_Condition,CT_Saut,CT_Lettrage,CT_ValidEch,CT_Sommeil,DE_No,cbDE_No,CT_ControlEnc,CT_NotRappel,N_Analytique,cbN_Analytique,CA_Num,CT_Telephone,CT_Telecopie,CT_EMail,CT_Site,CT_Coface,CT_Surveillance,CT_SvDateCreate,CT_SvFormeJuri,CT_SvEffectif,CT_SvCA,CT_SvResultat,CT_SvIncident,CT_SvDateIncid,CT_SvPrivil,CT_SvRegul,CT_SvCotation,CT_SvDateMaj,CT_SvObjetMaj,CT_SvDateBilan,CT_SvNbMoisBilan,N_AnalytiqueIFRS,cbN_AnalytiqueIFRS,CA_NumIFRS,CT_PrioriteLivr,CT_LivrPartielle,MR_No,cbMR_No,CT_NotPenal,EB_No,cbEB_No,CT_NumCentrale,CT_DateFermeDebut,CT_DateFermeFin,CT_FactureElec,CT_TypeNIF,CT_RepresentInt,CT_RepresentNIF,CT_EdiCodeType,CT_EdiCode,CT_EdiCodeSage,CT_ProfilSoc,CT_StatutContrat,CT_DateMAJ,CT_EchangeRappro,CT_EchangeCR,PI_NoEchange,cbPI_NoEchange,CT_BonAPayer,CT_DelaiTransport,CT_DelaiAppro,CT_LangueISO2,CT_AnnulationCR,CT_Facebook,CT_LinkedIn,CT_ExclureTrait,CT_GDPR,CT_Prospect,CT_OrderDay01,CT_OrderDay02,CT_OrderDay03,CT_OrderDay04,CT_OrderDay05,CT_OrderDay06,CT_OrderDay07,CT_DeliveryDay01,CT_DeliveryDay02,CT_DeliveryDay03,CT_DeliveryDay04,CT_DeliveryDay05,CT_DeliveryDay06,CT_DeliveryDay07,CAL_No,cbCAL_No,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq 
VALUES (@CT_Num,@CT_Intitule,@CT_Type,@CG_NumPrinc,@CT_Qualite,@CT_Classement,@CT_Contact,@CT_Adresse,@CT_Complement,@CT_CodePostal,@CT_Ville,@CT_CodeRegion,@CT_Pays,@CT_Raccourci,@BT_Num,@N_Devise,@CT_Ape,@CT_Identifiant,@CT_Siret,@CT_Statistique01,@CT_Statistique02,@CT_Statistique03,@CT_Statistique04,@CT_Statistique05,@CT_Statistique06,@CT_Statistique07,@CT_Statistique08,@CT_Statistique09,@CT_Statistique10,@CT_Commentaire,@CT_Encours,@CT_Assurance,@CT_NumPayeur,@N_Risque,@CO_No,@cbCO_No,@N_CatTarif,@CT_Taux01,@CT_Taux02,@CT_Taux03,@CT_Taux04,@N_CatCompta,@N_Period,@CT_Facture,@CT_BLFact,@CT_Langue,@N_Expedition,@N_Condition,@CT_Saut,@CT_Lettrage,@CT_ValidEch,@CT_Sommeil,@DE_No,@cbDE_No,@CT_ControlEnc,@CT_NotRappel,@N_Analytique,@cbN_Analytique,@CA_Num,@CT_Telephone,@CT_Telecopie,@CT_EMail,@CT_Site,@CT_Coface,@CT_Surveillance,@CT_SvDateCreate,@CT_SvFormeJuri,@CT_SvEffectif,@CT_SvCA,@CT_SvResultat,@CT_SvIncident,@CT_SvDateIncid,@CT_SvPrivil,@CT_SvRegul,@CT_SvCotation,@CT_SvDateMaj,@CT_SvObjetMaj,@CT_SvDateBilan,@CT_SvNbMoisBilan,@N_AnalytiqueIFRS,@cbN_AnalytiqueIFRS,@CA_NumIFRS,@CT_PrioriteLivr,@CT_LivrPartielle,@MR_No,@cbMR_No,@CT_NotPenal,@EB_No,@cbEB_No,@CT_NumCentrale,@CT_DateFermeDebut,@CT_DateFermeFin,@CT_FactureElec,@CT_TypeNIF,@CT_RepresentInt,@CT_RepresentNIF,@CT_EdiCodeType,@CT_EdiCode,@CT_EdiCodeSage,@CT_ProfilSoc,@CT_StatutContrat,@CT_DateMAJ,@CT_EchangeRappro,@CT_EchangeCR,@PI_NoEchange,@cbPI_NoEchange,@CT_BonAPayer,@CT_DelaiTransport,@CT_DelaiAppro,@CT_LangueISO2,@CT_AnnulationCR,@CT_Facebook,@CT_LinkedIn,@CT_ExclureTrait,@CT_GDPR,@CT_Prospect,@CT_OrderDay01,@CT_OrderDay02,@CT_OrderDay03,@CT_OrderDay04,@CT_OrderDay05,@CT_OrderDay06,@CT_OrderDay07,@CT_DeliveryDay01,@CT_DeliveryDay02,@CT_DeliveryDay03,@CT_DeliveryDay04,@CT_DeliveryDay05,@CT_DeliveryDay06,@CT_DeliveryDay07,@CAL_No,@cbCAL_No,@cbCreateur,@cbCreationUser);
SELECT @cbMarq = cbMarq FROM @TableMarq;

SELECT @LI_No = MAX(LI_No) + 1 FROM F_LIVRAISON WITH (NOLOCK)

INSERT INTO F_LIVRAISON (LI_No,CT_Num,LI_Intitule,LI_Pays,N_Expedition,N_Condition,cbCreateur,cbCreationUser)
VALUES (@LI_No,@CT_Num,@CT_Intitule,N'COTE D''IVOIRE',@N_Expedition,@N_Condition,@cbCreateur,@cbCreationUser);

INSERT INTO F_COMPTETG (CT_Num,CG_Num,cbCreateur,cbCreationUser)
VALUES (@CT_Num,@CG_NumPrinc,@cbCreateur,@cbCreationUser);

END ELSE BEGIN
	PRINT 'GIRDI 175'
	IF ISNULL(@CT_Qualite, '') <> CAST(@ENTITY_ID AS varchar(17)) BEGIN
		UPDATE dbo.F_COMPTET SET CT_Qualite = CAST(@ENTITY_ID AS varchar(17)) WHERE CT_Num = @CT_Num
	END

END

SET NOCOUNT OFF
--SELECT @cbMarq AS ID

END
--SELECT * FROM F_DEPOT
--SELECT * FROM F_COMPTET