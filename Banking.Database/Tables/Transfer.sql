﻿CREATE TABLE [dbo].[Transfer]
(
	[TransferId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[FromAccountId] UNIQUEIDENTIFIER NOT NULL,
	[ToAccountId] UNIQUEIDENTIFIER NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[Amount] MONEY NOT NULL,

	CONSTRAINT CHK_Transfer_Amount CHECK(Amount > 0)
)
