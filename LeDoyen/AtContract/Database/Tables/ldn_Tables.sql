SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON
GO

EXEC dbo.ldn_DropTables 'ldn_'
GO



CREATE TABLE [dbo].[ldn_Languages](
	[LanguageID] [int] IDENTITY(1,1) NOT NULL,
	[CultureName] [varchar](5) NOT NULL,
	[CultureID] [int] NOT NULL,
	[LanguageName] [nvarchar](32) NOT NULL,
	[NativeLanguageName] [nvarchar](64) NOT NULL,
	CONSTRAINT [PK_ldn_Languages] PRIMARY KEY CLUSTERED ([LanguageID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/*
CREATE TABLE [dbo].[ldn_Addresses](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[LanguageID] [int] NOT NULL,
	[PostCode] [varchar](8) NOT NULL,
	[CountryName] [nvarchar](32) NOT NULL,
	[RegionName] [nvarchar](48) NOT NULL,
	[CityName] [nvarchar](48) NOT NULL,
	[AddressLine1] [nvarchar](128) NOT NULL,
	[AddressLine2] [nvarchar](128),
	
 CONSTRAINT [PK_ldn_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ldn_Addresses]  WITH CHECK ADD  CONSTRAINT [FK_ldn_Addresses_ldn_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[ldn_Languages] ([LanguageID])

ALTER TABLE [dbo].[ldn_Addresses] CHECK CONSTRAINT [FK_ldn_Addresses_ldn_Languages]


CREATE TABLE [dbo].[ldn_Persons](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](128) NOT NULL,
	[MiddleName] [nvarchar](128) NOT NULL,
	[LastName] [nvarchar](128) NOT NULL,
	[BirthDate] [date] NULL,
	[TaxID] [char](10) NULL,
 CONSTRAINT [PK_ldn_Persons] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[ldn_PersonAddresses](
	[PersonID] [int] NOT NULL,
	[AddressID] [int] NOT NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[ldn_PersonAddresses]  WITH CHECK ADD  CONSTRAINT [FK_ldn_PersonAddresses_ldn_Addresses] FOREIGN KEY([AddressID])
REFERENCES [dbo].[ldn_Addresses] ([AddressID])

ALTER TABLE [dbo].[ldn_PersonAddresses] CHECK CONSTRAINT [FK_ldn_PersonAddresses_ldn_Addresses]

ALTER TABLE [dbo].[ldn_PersonAddresses]  WITH CHECK ADD  CONSTRAINT [FK_ldn_PersonAddresses_ldn_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[ldn_Persons] ([PersonID])

ALTER TABLE [dbo].[ldn_PersonAddresses] CHECK CONSTRAINT [FK_ldn_PersonAddresses_ldn_Persons]

CREATE PRIMARY KEY CLUSTERED INDEX PK_ldn_PersonAddresses ON dbo.ldn_PersonAddresses
	(
	PersonID,
	AddressID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	
*/
	
CREATE TABLE [dbo].[ldn_Contractors](
	[ContractorID] [int] IDENTITY(1,1) NOT NULL,
	
	[FirstName] [nvarchar](128) NOT NULL,
	[MiddleName] [nvarchar](128) NOT NULL,
	[LastName] [nvarchar](128) NOT NULL,
	[EnglishFullName] [nvarchar](256) NOT NULL,
	
	[TaxID] [char](10) NULL,
	[BirthDate] [date] NULL,
	
	[AddressEn] [nvarchar](256) NOT NULL,
	[AddressUk] [nvarchar](256) NOT NULL,
	
	[PassportNumber] [nvarchar](8) NOT NULL,
	[PassportIssuedBy] [nvarchar](128) NOT NULL,
	[PassportIssuedDate] [date] NOT NULL,
		
	CONSTRAINT [PK_ldn_Contractors] PRIMARY KEY CLUSTERED ([ContractorID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX IX_ldn_Contractors_TaxID ON dbo.ldn_Contractors(TaxID)
	WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX IX_ldn_Contractors_PassportNumber ON dbo.ldn_Contractors(PassportNumber)
	WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


CREATE TABLE [dbo].[ldn_Projects](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	
	[TypeNameUk] [nvarchar](64) NOT NULL,
	[NameUk] [nvarchar](128) NOT NULL,
	[WorkNameUk] [nvarchar](128) NOT NULL,
	
	[TypeNameEn] [nvarchar](64) NOT NULL,
	[NameEn] [nvarchar](128) NOT NULL,
	[WorkNameEn] [nvarchar](128) NOT NULL,
	
	CONSTRAINT [PK_ldn_Projects] PRIMARY KEY CLUSTERED ([ProjectID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[ldn_PaymentTypes](
	[PaymentTypeID] [int] IDENTITY(1,1) NOT NULL,
	
	[NameEn] [nvarchar](128) NOT NULL,
	[NameUa] [nvarchar](128) NOT NULL,
	
	[NameGenitiveEn] [nvarchar](128) NOT NULL,
	[NameGenitiveUa] [nvarchar](128) NOT NULL,
	
	CONSTRAINT [PK_ldn_PaymentTypes] PRIMARY KEY CLUSTERED ([PaymentTypeID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ldn_Agreements](
	[AgreementID] [int] IDENTITY(1,1) NOT NULL,
	
	[AgreementNumber] [int] NOT NULL,
	
	[AgreementDate] [date] NOT NULL,
	[ServiceStartDate] [date] NOT NULL,
	[ServiceEndDate] [date] NOT NULL,
	
	[PaymentDueDate] [date] NOT NULL,

	[ProjectID] int NOT NULL,
	[ContractorID] int NOT NULL,
	[PaymentTypeID] int NOT NULL,
	
	CONSTRAINT [PK_ldn_Agreements] PRIMARY KEY CLUSTERED ([AgreementID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
	CONSTRAINT [FK_ldn_Agreements_ldn_Projects] FOREIGN KEY([ProjectID]) REFERENCES [dbo].[ldn_Projects] ([ProjectID]),
	CONSTRAINT [FK_ldn_Agreements_ldn_Contractors] FOREIGN KEY([ContractorID]) REFERENCES [dbo].[ldn_Contractors] ([ContractorID]),
	CONSTRAINT [FK_ldn_Agreements_ldn_PaymentTypes] FOREIGN KEY([PaymentTypeID]) REFERENCES [dbo].[ldn_PaymentTypes] ([PaymentTypeID])
) ON [PRIMARY]


