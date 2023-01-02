namespace Dapper.WebApi.Services.Queries
{
    public interface ICommandText
    {
        string GetTrans { get; }
        string GetTransById { get; }
        string GetTransByCurrencyCode { get; }
        string GetTransByDateRange { get; }
        string GetTransByStatus { get; }
        string AddTrans { get; }
        string UpdateTrans { get; }
        string RemoveTrans { get; }
        string GetTransByIdSp { get; }
    }
}
