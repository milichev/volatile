SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON
SET NOCOUNT ON
GO

SET IDENTITY_INSERT [dbo].[ldn_Languages] ON

INSERT INTO [dbo].[ldn_Languages]([LanguageID],[CultureName],[CultureID],[LanguageName],[NativeLanguageName])
VALUES(1,'',0x007F,N'(invariant culture)','')

INSERT INTO [dbo].[ldn_Languages]([LanguageID],[CultureName],[CultureID],[LanguageName],[NativeLanguageName])
VALUES(2,'en',0x0009,N'English',N'English')

INSERT INTO [dbo].[ldn_Languages]([LanguageID],[CultureName],[CultureID],[LanguageName],[NativeLanguageName])
VALUES(3,'uk-UA',0x0422,N'Ukraininan',N'Українська')

INSERT INTO [dbo].[ldn_Languages]([LanguageID],[CultureName],[CultureID],[LanguageName],[NativeLanguageName])
VALUES(4,'ru-RU',0x0419,N'Russian',N'Русский')

SET IDENTITY_INSERT [dbo].[ldn_Languages] OFF


SET IDENTITY_INSERT [dbo].[ldn_PaymentTypes] ON

INSERT INTO [dbo].[ldn_PaymentTypes]([PaymentTypeID],[NameEn],[NameUa],[NameGenitiveEn],[NameGenitiveUa])
VALUES(1,N'Cash',N'готівковий розрахунок',N'by Cash',N'готівкового розрахунку')

INSERT INTO [dbo].[ldn_PaymentTypes]([PaymentTypeID],[NameEn],[NameUa],[NameGenitiveEn],[NameGenitiveUa])
VALUES(2,N'Money Order',N'грошовий переказ',N'by Money Order',N'грошового переказу')

INSERT INTO [dbo].[ldn_PaymentTypes]([PaymentTypeID],[NameEn],[NameUa],[NameGenitiveEn],[NameGenitiveUa])
VALUES(3,N'Personal Check',N'особистий чек',N'by Personal Check',N'особистого чека')

INSERT INTO [dbo].[ldn_PaymentTypes]([PaymentTypeID],[NameEn],[NameUa],[NameGenitiveEn],[NameGenitiveUa])
VALUES(4,N'Credit Card',N'кредитна карта',N'by Credit Card',N'кредитної карти')

SET IDENTITY_INSERT [dbo].[ldn_Languages] OFF

