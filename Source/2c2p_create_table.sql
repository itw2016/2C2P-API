CREATE TABLE [dbo].[Trans] (
    [TransactionId]   NCHAR (50)      NULL,
    [Amount]          DECIMAL (20, 2) NULL,
    [CurrencyCode]    NCHAR (10)      NULL,
    [TransactionDate] DATETIME        NULL,
    [Status]          NCHAR (10)      NULL
);