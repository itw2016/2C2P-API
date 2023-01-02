namespace Dapper.WebApi.Services.Queries
{
    public class CommandText : ICommandText
    {
        public string GetTrans => "Select * from Trans";
        public string GetTransById => "Select * from Trans where Id= @Id";
        public string GetTransByCurrencyCode => "Select * from Trans where CurrencyCode= @CurrencyCode";
        public string GetTransByDateRange => "Select * from Trans where TransactionDate BETWEEN @StartDate AND @EndDate";
        public string GetTransByStatus => "Select * from Trans where UPPER(RTRIM(LTRIM(Status)))= @Status";
        public string AddTrans => "Insert into  [2C2P].[dbo].[Trans] (TransactionId, Amount, CurrencyCode, TransactionDate, Status) values (@TransactionId, @Amount, @CurrencyCode, @TransactionDate, @Status)";
        public string UpdateTrans => "Update [2C2P].[dbo].[Trans] set Amount = @Amount, CurrencyCode = @CurrencyCode, Status=@Status,TransactionDate = GETDATE() where Id =@Id";
        public string RemoveTrans => "Delete from [Dapper].[dbo].[Product] where Id= @Id";
        public string GetTransByIdSp => "GetTransId";

    }
}
