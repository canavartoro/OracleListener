
ALTER PROCEDURE dbo.CREATE_DEPOT(
	@WHOUSE_ID INTEGER,
	@WHOUSE_CODE NVARCHAR(60),
	@DESCRIPTION NVARCHAR(150)
)
AS BEGIN
DECLARE
@DE_No int = @WHOUSE_ID,
@DE_Intitule varchar(35) = @DESCRIPTION,
@DE_Adresse varchar(35) = NULL,
@DE_Complement varchar(35) = NULL,
@DE_CodePostal varchar(9) = NULL,
@DE_Ville varchar(35) = NULL,
@DE_Contact varchar(35) = NULL,
@DE_Principal smallint = 1,
@DE_CatCompta smallint = 1,
@DE_Region varchar(25) = NULL,
@DE_Pays varchar(35) = N'Côte d''Ivoire',
@DE_EMail varchar(69) = NULL,
@DE_Code varchar(9) = @WHOUSE_CODE,
@DE_Telephone varchar(21) = NULL,
@DE_Telecopie varchar(21) = NULL,
@DE_Replication int = 0,
@DP_NoDefaut int = NULL,
@cbDP_NoDefaut int = NULL,
@DE_Exclure smallint = 0,
@DE_Souche01 smallint = 0,
@DE_Souche02 smallint = 0,
@DE_Souche03 smallint = 0,
@cbProt smallint = NULL,
@cbMarq int = NULL,
@cbCreateur char(4) = N'ERP1',
@cbModification datetime = NULL,
@cbReplication int = NULL,
@cbFlag smallint = NULL,
@cbCreation datetime = GETDATE(),
@cbCreationUser uniqueidentifier = '77384016-921F-472F-B56D-1D563B7DDF3C'

IF EXISTS (SELECT * FROM dbo.F_DEPOT WITH (NOLOCK) WHERE DE_Code = @DE_Code) BEGIN
	UPDATE dbo.F_DEPOT SET DE_Intitule = @DE_Intitule WHERE DE_Code = @DE_Code
END ELSE BEGIN
	DECLARE @TableMarq TABLE (cbMarq int);
	INSERT INTO F_DEPOT (DE_No,DE_Intitule,DE_Adresse,DE_Complement,DE_CodePostal,DE_Ville,DE_Contact,DE_Principal,DE_CatCompta,DE_Region,DE_Pays,DE_EMail,DE_Code,DE_Telephone,DE_Telecopie,DE_Replication,DP_NoDefaut,cbDP_NoDefaut,DE_Exclure,DE_Souche01,DE_Souche02,DE_Souche03,cbCreateur,cbCreationUser) OUTPUT inserted.cbMarq INTO @TableMarq 
	VALUES (@DE_No,@DE_Intitule,@DE_Adresse,@DE_Complement,@DE_CodePostal,@DE_Ville,@DE_Contact,@DE_Principal,@DE_CatCompta,@DE_Region,@DE_Pays,@DE_EMail,@DE_Code,@DE_Telephone,@DE_Telecopie,@DE_Replication,@DP_NoDefaut,@cbDP_NoDefaut,@DE_Exclure,@DE_Souche01,@DE_Souche02,@DE_Souche03,@cbCreateur,@cbCreationUser);
	SELECT @cbMarq = cbMarq FROM @TableMarq;

	INSERT INTO F_DEPOTEMPL (DE_No,DP_No,DP_Code,DP_Intitule,DP_Zone,DP_Type,cbCreateur,cbCreationUser) --OUTPUT inserted.cbMarq INTO @TableMarq 
	VALUES (@DE_No,@DE_No,@DE_Code,@DE_Intitule,1,0,@cbCreateur,@cbCreationUser); 
	--SELECT cbMarq FROM @TableMarq;

END

END