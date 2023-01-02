using Microsoft.AspNetCore.Http;

namespace Dapper.WebApi.Models
{
    public class PostRequest
    {
        public IFormFile TransactionFile { get; set; }
    }
}
